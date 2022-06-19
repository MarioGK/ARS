namespace ARS.Common.Services;

public class CancellationService
{
    public CancellationService()
    {
        Source = new CancellationTokenSource();
        Token = Source.Token;
    }

    public CancellationTokenSource Source { get; set; }
    public CancellationToken Token { get; set; }
}