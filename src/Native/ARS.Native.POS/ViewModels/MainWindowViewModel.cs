namespace Store.UI.POS.ViewModels;

[SingleInstanceView]
public class MainWindowViewModel : ViewModelBase, IScreen
{
    private readonly LoginViewModel _loginViewModel;
    private readonly SalesViewModel _salesViewModel;

    public MainWindowViewModel()
    {
        _salesViewModel = new SalesViewModel
        {
            HostScreen = this
        };
        _loginViewModel = new LoginViewModel
        {
            HostScreen = this
        };

        GoLogin();

        MessageBus.Current.Listen<string>("GoLogin")
            .Subscribe(x => GoLogin());

        MessageBus.Current.Listen<string>("GoSales")
            .Subscribe(x => GoSales());
    }

    //public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }
    public RoutingState Router { get; } = new();

    public void GoLogin()
    {
        Router.NavigateAndReset.Execute(_loginViewModel);
    }

    public void GoSales()
    {
        Router.NavigateAndReset.Execute(_salesViewModel);
    }
}