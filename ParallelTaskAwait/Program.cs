using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

// 通常の測定
MeasureTime(NormalTask, 5);

// 並列タスクの測定
MeasureTime(NormalTaskAwait, 5);

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

async Task NormalTaskAwait()
{
    var loopNum = 10000;
    var arraySize = 100000;
    // 時間のかかる処理を3種類実行するイメージ
    await UseNormalForLoopAwait(loopNum, arraySize);
    await UseNormalForLoopAwait(loopNum, arraySize);
    await UseNormalForLoopAwait(loopNum, arraySize);
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