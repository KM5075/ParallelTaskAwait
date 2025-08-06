using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

// 通常の測定
MeasureTime(NormalTask, 5);

// 非同期タスクの順次実行の測定
await MeasureTime2(NormalTaskAwait, 5);

// 非同期タスクの並列実行の測定
await MeasureTime2(ParallelTaskAwait, 5);


void NormalTask()
{
    var loopNum = 10000;
    var arraySize = 100000;
    // 時間のかかる処理を3種類実行するイメージ
    UseNormalForLoop(loopNum, arraySize);
    UseNormalForLoop(loopNum, arraySize);
    UseNormalForLoop(loopNum, arraySize);
}

void UseNormalForLoop(int loopNum, int arraySize)
{
    Console.WriteLine("Normal For Loop");
    for (int j = 0; j < loopNum; j++)
    {
        int[] values = new int[arraySize];

        // Multiply each element by 2
        for (int i = 0; i < values.Length; i++)
        {
            values[i] *= 2;
        }

        // Display the first 10 values
        for (int i = 0; i < 10; i++)
        {
            var value = values[i];
        }
    }
    Console.WriteLine("Normal For Loop Completed");
}

/// <summary>
/// awaitを使った通常のタスクの順次実行
/// </summary>
/// <returns></returns>
async Task NormalTaskAwait()
{
    var loopNum = 10000;
    var arraySize = 100000;
    // 時間のかかる処理を3種類実行するイメージ
    await UseNormalForLoopAwait(loopNum, arraySize);
    await UseNormalForLoopAwait(loopNum, arraySize);
    await UseNormalForLoopAwait(loopNum, arraySize);
}

async Task ParallelTaskAwait()
{
    var loopNum = 10000;
    var arraySize = 100000;
    // 並列で実行
    var tasks = new[]
    {
        Task.Run(() => UseNormalForLoopAwait(loopNum, arraySize)),
        Task.Run(() => UseNormalForLoopAwait(loopNum, arraySize)),
        Task.Run(() => UseNormalForLoopAwait(loopNum, arraySize))
    };

    await Task.WhenAll(tasks);

    //// 時間のかかる処理を3種類実行するイメージ
    //await Task.WhenAll(
    //    UseNormalForLoopAwait(loopNum, arraySize),
    //    UseNormalForLoopAwait(loopNum, arraySize),
    //    UseNormalForLoopAwait(loopNum, arraySize)
    //);
}

async Task UseNormalForLoopAwait(int loopNum, int arraySize)
{
    Console.WriteLine("Normal For Loop");
    for (int j = 0; j < loopNum; j++)
    {
        int[] values = new int[arraySize];

        // Multiply each element by 2
        for (int i = 0; i < values.Length; i++)
        {
            values[i] *= 2;
        }

        // Display the first 10 values
        for (int i = 0; i < 10; i++)
        {
            var value = values[i];
        }
    }
    Console.WriteLine("Normal For Loop Completed");
}


void MeasureTime(Action action, int times)
{
    int time = 0;
    for (int i = 0; i < times; i++)
    {
        time += MeasureTime(action);
    }

    Console.WriteLine("Average Time taken: {0}ms", time / times);
    Console.WriteLine();

    static int MeasureTime(Action action)
    {
        var sw = new Stopwatch();
        sw.Start();
        action();
        sw.Stop();
        Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        return (int)sw.Elapsed.TotalMilliseconds;
    }
}

async Task MeasureTime2(Func<Task> normalTaskAwait, int v)
{
    int totalTime = 0;
    for (int i = 0; i < v; i++)
    {
        var sw = new Stopwatch();
        sw.Start();
        await normalTaskAwait();
        sw.Stop();
        Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        totalTime += (int)sw.Elapsed.TotalMilliseconds;
    }
    Console.WriteLine("Average Time taken: {0}ms", totalTime / v);
    Console.WriteLine();
}