using System.Runtime.CompilerServices;

namespace APA.QuickSort.Lib;

public readonly struct QuickSelect
{
    private static readonly Random Random = new(Random.Shared.Next());
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static int Select(List<int> list, int left, int right, int k)
    {
        while (true)
        {
            if (left == right) return list[left];

            var pivotIndex = Random.Next(left, right);
            pivotIndex = Partition(list, left, right, pivotIndex);

            if (k == pivotIndex)
            {
                return k;
            }

            if (k < pivotIndex)
            {
                right = pivotIndex - 1;
                continue;
            }

            left = pivotIndex + 1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private static int Partition(IList<int> list, int left, int right, int pivotIndex)
    {
        var pivotValue = list[pivotIndex];
        (list[pivotIndex], list[right]) = (list[right], list[pivotIndex]); 
        var storeIndex = left;

        for (var i = left; i < right; i++)
        {
            if (list[i] >= pivotValue) continue;
            (list[storeIndex], list[i]) = (list[i], list[storeIndex]);
            storeIndex++;
        }

        (list[right], list[storeIndex]) = (list[storeIndex], list[right]); 
        return storeIndex;
    }
}