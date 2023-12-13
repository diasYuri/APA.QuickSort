using System.Runtime.CompilerServices;

namespace APA.QuickSort.Lib;

public readonly struct RandomSet
{
    private static readonly Random Random = new(Random.Shared.Next());
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static List<int> GenerateList(int size, double disorder)
    {
        var list = Enumerable.Range(1, size).ToList();
        var swapCount = (int)(size * disorder);

        for (var i = 0; i < swapCount; i++)
        {
            var pos1 = Random.Next(size);
            var pos2 = Random.Next(size);
            (list[pos1], list[pos2]) = (list[pos2], list[pos1]);
        }

        return list;
    }
}