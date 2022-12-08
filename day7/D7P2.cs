namespace day7;

public static class D7P2
{
    public static long Part2Answer(this string input) =>
        input
            .ParseThings()
            .CreateDirectoryTree()
            .GetDirectorySizes()
            .ToArray()
            .GetSizeOfSmallestDirNeededToGetEnoughSpace();

    internal static long GetFreeDiskSpace(this IEnumerable<(Directory Dir, long TotalSize)> dirs)
    {
        long totalDiskSpace = 70000000;
        var sizeOfLargestFolder = dirs.Select(d => d.TotalSize).Max();
        return totalDiskSpace - sizeOfLargestFolder;
    }

    internal static long GetSizeOfSmallestDirNeededToGetEnoughSpace(this ICollection<(Directory Dir, long TotalSize)> dirs)
    {
        long neededFreeSpace = 30000000;
        var currentlyFree = dirs.GetFreeDiskSpace();
        var extraSpaceNeeded = neededFreeSpace - currentlyFree;
        return dirs.GetSizeOfSmallestDirWithAtLeastThisSize(extraSpaceNeeded);
    }

    internal static long GetSizeOfSmallestDirWithAtLeastThisSize(this IEnumerable<(Directory Dir, long TotalSize)> dirs, long minSize)
    {
        return dirs.Where(d => d.TotalSize >= minSize).MinBy(d => d.TotalSize).TotalSize;
    }

}
