using System.Reflection;

namespace Store.UI.POS;

public class App : Application
{
    public static MainWindow MainWindow { get; set; } = null!;

    public static Assembly StoreUI { get; set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
        var coreAssemblyName = assemblies.FirstOrDefault(a => a.Name == "Store.UI")!;
        StoreUI = Assembly.Load(coreAssemblyName);
        var executingAssembly = Assembly.GetExecutingAssembly();

        Locator.CurrentMutable.RegisterViewsForViewModels(StoreUI);
        Locator.CurrentMutable.RegisterViewsForViewModels(executingAssembly);
        //Locator.CurrentMutable.RegisterViewModels(executingAssembly);
        //Locator.CurrentMutable.RegisterScreen(executingAssembly);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };

            desktop.MainWindow = MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}