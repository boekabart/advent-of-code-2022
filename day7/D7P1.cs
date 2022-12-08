using shared;
using static System.String;

namespace day7;

public static class D7P1
{
    public static long Part1Answer(this string input) =>
        input
            .ParseThings()
            .CreateDirectoryTree()
            .GetDirectorySizes()
            .SumOfDirectoriesWithLessThan100000TotalBytes();

    internal static IEnumerable<IThing> ParseThings(this string input) =>
        input
            .TrimmedLines()
            .Select(TryParseAsThing)
            .OfType<IThing>();

    internal static IThing? TryParseAsThing(this string line)
    {
        if (IsNullOrWhiteSpace(line)) return null;
        if (line.Equals("$ cd /")) return new CdRoot();
        if (line.Equals("$ cd ..")) return new CdUp();
        if (line.StartsWith("$ cd ")) return new CdThing(line.Substring(5));
        if (line.StartsWith("$")) return null;
        if (line.StartsWith("dir ")) return new DirThing(line.Substring(4));
        var split = line.Split(' ');
        if (split.Length != 2) return null;
        if (!long.TryParse(split[0], out var size))
            return null;
        return new FileThing(split[1], size);
    }

    internal static Directory CreateDirectoryTree(this IEnumerable<IThing> src) =>
        src.Aggregate(new Directory(), (current, item) => item switch
        {
            CdUp => current.Parent(),
            CdThing cd => current.AddDirectory(cd.SubDir),
            CdRoot => current.Root(),
            FileThing file => current.AddFile(file),
            _ => current
        }).Root();

    private static Directory AddFile(this Directory currentDir, FileThing file)
    {
        currentDir.Files.Add(new(file.Name, file.Size, currentDir));
        return currentDir;
    }

    internal static Directory Root(this Directory root) => root is SubDirectory dir ? dir.Parent.Root() : root;
    internal static Directory Parent(this Directory dir) => dir is SubDirectory subDir ? subDir.Parent : dir;

    internal static SubDirectory AddDirectory(this Directory parent, string name)
    {
        var newDir = new SubDirectory(name, parent);
        parent.SubDirectories.Add(newDir);
        return newDir;
    }

    internal static IEnumerable<(Directory Dir, long TotalSize)> GetDirectorySizes(this Directory root)
    {
        var subDirs = root.SubDirectories.SelectMany(sd => sd.GetDirectorySizes()).ToList();
        var fileSizes = root.Files.Sum(f => f.Size);
        var dirSizes = subDirs
            .Where(sd => root.SubDirectories.Contains(sd.Dir))
            .Sum(x => x.TotalSize);
        var myItem = (root, fileSizes + dirSizes);
        return subDirs.Prepend(myItem);
    }

    internal static long SumOfDirectoriesWithLessThan100000TotalBytes(this IEnumerable<(Directory Dir, long TotalSize)> things) => things
        .Where(pair => pair.TotalSize <= 100000)
        .Sum(pair => pair.TotalSize);
}

internal interface IThing {}

internal record DirThing(string Name) : IThing;

internal record FileThing(string Name, long Size) : IThing;

internal record CdRoot : IThing;

internal record CdUp : IThing;

internal record CdThing(string SubDir) : IThing;

internal record Directory(string Name = "")
{
    internal List<File> Files { get; } = new();
    internal List<SubDirectory> SubDirectories { get; } = new();
}

internal record SubDirectory(string Name, Directory Parent) : Directory(Name);

internal record File(string Name, long Size, Directory Directory);
