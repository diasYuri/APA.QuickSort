namespace APA.QuickSort.Lib;

public readonly struct QuickSort
{
    private static readonly Random Random = new(Random.Shared.Next());

    public static void Sort(List<int> list, int left, int right, int pivotType)
    {
        while (true)
        {
            if (left >= right) return;
            var pi = Partition(list, left, right, pivotType);
            Sort(list, left, pi - 1, pivotType);
            left = pi + 1;
        }
    }

    private static int Partition(List<int> list, int left, int right, int pivotType)
    {
        var pivotIndex = ChoosePivot(list, left, right, pivotType);
        var pivot = list[pivotIndex];
        
        (list[pivotIndex], list[right]) = (list[right], list[pivotIndex]);

        var i = left - 1;
        for (var j = left; j < right; j++)
        {
            if (list[j] >= pivot) continue;
            i++;
            Swap(list, i, j);
        }

        Swap(list, i + 1, right);
        return i + 1;
    }

    private static int ChoosePivot(List<int> list, int left, int right, int pivotType) =>
        pivotType switch
        {
            1 => left,
            2 => (left + right) / 2,
            3 => MedianOfThree(list, left, (left + right) / 2, right),
            4 => Random.Next(left, right),
            5 => QuickSelect.Select(list, left, right, (right - left) / 2 + left),
            6 => FindPivot(list, left, right),
            _ => right
        };

    private static int FindPivot(IReadOnlyList<int> list, int left, int right)
    {
        var last = list[left];
        for (var i = left+1; i < right-1; i++)
        {
            if (list[i] < last) return i+1;
            last = list[i];
        }

        return right - 1;
    }
    
    public static int MedianOfThree(IList<int> arr, int a, int b, int c)
    {
        if(arr[a] < arr[b])
        {
            if(arr[b] < arr[c]) return b;
            if(arr[a] < arr[c]) return c;
            return a;
        }

        if(arr[c] < arr[b]) return b;
        if(arr[c] < arr[a]) return c;
        return a;
    }

    
    private static void Swap(IList<int> arr, int i, int j) => (arr[i], arr[j]) = (arr[j], arr[i]);
}