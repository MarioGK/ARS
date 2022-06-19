using System.Globalization;

namespace Store.UI.POS;

internal class Program
{
    [STAThread]
    public static int Main(string[] args)
    {
        var brCulture = new CultureInfo("br-BR");

        CultureInfo.CurrentCulture = brCulture;
        Thread.CurrentThread.CurrentCulture = brCulture;

        return BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseSkia()
            .UseReactiveUI();
    }
}