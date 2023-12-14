using System.Diagnostics;
using APA.QuickSort.Lib;

var lengths = new[]{100, 1000, 10_000, 100_000};
var disorders = new[]{0.05, 0.25, 0.45};
static void Init()
{
    var filestream = new FileStream("output.txt", FileMode.Append);
    var streamWriter = new StreamWriter(filestream)
    {
        AutoFlush = true
    };
    Console.SetOut(streamWriter);
    Console.SetError(streamWriter);
}


Init();
var sw = new Stopwatch();

foreach (var length in lengths)
{
    foreach (var disorder in disorders)
    {
        for (var j = 0; j < 15; j++)
        {
            var list = RandomSet.GenerateList(length, disorder);
            
            for (var i = 1; i <= 6; i++)
            {
                var listCopy = new List<int>(list);
                var noGcRegion = GC.TryStartNoGCRegion(512_000);
                sw.Start();
                QuickSort.Sort(listCopy, 0, list.Count-1, i);
                sw.Stop();
                if(noGcRegion) GC.EndNoGCRegion();
                Console.WriteLine($"Length: {length} Disorder: {disorder} Iteration: {j} Pivot type: {i} took {sw.Elapsed.TotalNanoseconds} ns");
                sw.Reset();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
    
        
