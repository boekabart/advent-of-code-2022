using System.Diagnostics;

namespace day7;

public interface IThing
{
    string? Path => null;
    long? Size => null;
}

public interface IFileSystemEntry : IThing
{
}

public record Dir(string Path) : IFileSystemEntry;
public record File(string Path, long? Size) : IFileSystemEntry;

public record Cd(string Path) : IThing;

public record Command() : IThing;

internal static class D7P1
{
    public static IEnumerable<IThing> ParseThings(this string input) =>
        input
            .Split(new[] {'\n'})
            .Select(s => s.Trim())
            .Select(TryParseAsThing)
            .OfType<IThing>();

    public static IThing? TryParseAsThing(this string line)
    {
        if (String.IsNullOrWhiteSpace(line)) return null;
        if (line.StartsWith("$ cd ")) return new Cd(line.Substring(5));
        if (line.StartsWith("$")) return null;
        if (line.StartsWith("dir ")) return new Dir(line.Substring(4));
        var split = line.Split(' ');
        if (split.Length != 2) return null;
        if (!long.TryParse(split[0], out var size))
            return null;
        return new File(split[1], size);
    }

    public static IEnumerable<IFileSystemEntry> ExpandPaths(this IEnumerable<IThing> src)
    {
        var dirs = new HashSet<string>();
        var currentDir = "/";
        foreach (var item in src)
        {
            var path = PathCombine(currentDir, item.Path!);
            if (item is Cd)
            {
                currentDir = path;
                if (dirs.Add(path))
                    yield return new Dir(path);
            }
            else if (item is File)
            {
                yield return new File(path, item.Size);
            }
           }
    }

    internal static string PathCombine(this string basis, string relative)
    {
        var pathCombine = Path.Combine(basis, relative);
        var full = Path.GetFullPath(pathCombine);
        var root = Path.GetPathRoot(full);
        var kindaFull = full.Replace(root, "/");
        return kindaFull.Replace("\\", "/");
    }

    public static IEnumerable<(Dir Dir, long TotalSize)> GetDirectorySizes(this IEnumerable<IFileSystemEntry> src)
    {
        return src.OfType<Dir>()
            .Select(d => (d, src
                .OfType<File>()
                .Where(f => IsPartOf(f,d))
                .Sum(f => f.Size.Value)));
    }

    private static bool IsPartOf(File file, Dir ir)
    {
        //return file.Path.StartsWith(ir.Path);
        return ir.Path == "/" || file.Path.StartsWith(ir.Path + "/");
    }

    public static long GetResult(this IEnumerable<(Dir Dir, long TotalSize)> things) => things
        .Where( pair => pair.TotalSize<=100000)
        .Sum( pair => pair.TotalSize);
}