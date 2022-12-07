using day7;

namespace day3;

internal static class D3P2
{
    public static long GetFreeDiskSpace(this IEnumerable<(Dir Dir, long TotalSize)> dirs)
    {
        long totalDiskSpace = 70000000;
        var sizeOfLargestFolder = dirs.Select(d => d.TotalSize).Max();
        return totalDiskSpace - sizeOfLargestFolder;
    }

    public static long GetSizeOfSmallestDirNeededToGetEnoughSpace(this IEnumerable<(Dir Dir, long TotalSize)> dirs)
    {
        long neededFreeSpace = 30000000;
        var currentlyFree = dirs.GetFreeDiskSpace();
        var extraSpaceNeeded = neededFreeSpace - currentlyFree;
        return dirs.GetSizeOfSmallestDirWithAtLeastThisSize(extraSpaceNeeded);
    }

    public static long GetSizeOfSmallestDirWithAtLeastThisSize(this IEnumerable<(Dir Dir, long TotalSize)> dirs, long minSize)
    {
        return dirs.Where(d => d.TotalSize >= minSize).MinBy(d => d.TotalSize).TotalSize;
    }

}
