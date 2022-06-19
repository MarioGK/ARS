using System.Diagnostics;

namespace ARS.Common.Helpers;

public class StopTimer : IDisposable
{
    private readonly string _message;
    private readonly Stopwatch _stopwatch;

    public StopTimer(string message = "")
    {
        _message = message;
        _stopwatch = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        _stopwatch.Stop();
        Console.WriteLine($"Completed {_message} in {_stopwatch.ElapsedMilliseconds} ms");
    }
}