using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace ARS.Store.Common;

public static class LogHelper
{
    private static readonly ConsoleTheme Theme = new SystemConsoleTheme(
        new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle>
        {
            [ConsoleThemeStyle.Text] = new()
            {
                Foreground = ConsoleColor.Gray
            },
            [ConsoleThemeStyle.SecondaryText] = new()
            {
                Foreground = ConsoleColor.DarkGray
            },
            [ConsoleThemeStyle.TertiaryText] = new()
            {
                Foreground = ConsoleColor.DarkGray
            },
            [ConsoleThemeStyle.Invalid] = new()
            {
                Foreground = ConsoleColor.Yellow
            },
            [ConsoleThemeStyle.Null] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.Name] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.String] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.Number] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.Boolean] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.Scalar] = new()
            {
                Foreground = ConsoleColor.White
            },
            [ConsoleThemeStyle.LevelVerbose] = new()
            {
                Foreground = ConsoleColor.Gray,
                Background = ConsoleColor.DarkGray
            },
            [ConsoleThemeStyle.LevelDebug] = new()
            {
                Foreground = ConsoleColor.Magenta
            },
            [ConsoleThemeStyle.LevelInformation] = new()
            {
                Foreground = ConsoleColor.Blue
            },
            [ConsoleThemeStyle.LevelWarning] = new()
            {
                Foreground = ConsoleColor.Yellow
            },
            [ConsoleThemeStyle.LevelError] = new()
            {
                Foreground = ConsoleColor.White,
                Background = ConsoleColor.Red
            },
            [ConsoleThemeStyle.LevelFatal] = new()
            {
                Foreground = ConsoleColor.White,
                Background = ConsoleColor.Red
            }
        });

    public static void SetupLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Debug()
            .WriteTo.Console(
                theme: Theme,
                outputTemplate:
                "|{Timestamp:HH:mm:ss.fff}| [{Level:u3}] {Message} ({SourceContext}){NewLine}{Exception}")
            .WriteTo.File("logs/log.txt",
                outputTemplate:
                "|{Timestamp:yyyy-MM-dd HH:mm:ss.fff}| [{Level:u3}] ({SourceContext}) {Message}{NewLine}START{Exception}END{NewLine}",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true)
            .CreateLogger();
    }
}