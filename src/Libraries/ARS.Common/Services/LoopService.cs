using ARS.Common.Interfaces;

namespace ARS.Common.Services;

public class LoopService : IService
{
    public LoopService()
    {
        Timer = new PeriodicTimer(TimeSpan);

        if (AutoStart)
        {
            Start();
        }
    }

    public bool AutoStart { get; set; } = true;
    public TimeSpan TimeSpan { get; set; } = TimeSpan.FromSeconds(1);

    public PeriodicTimer Timer { get; set; }

    public virtual async Task Run()
    {
    }

    public void Start()
    {
        Task.Run(TimerLoop);
    }

    private async Task TimerLoop()
    {
        while (await Timer.WaitForNextTickAsync()) await Run();
    }
}