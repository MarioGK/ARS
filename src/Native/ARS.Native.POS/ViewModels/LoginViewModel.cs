using Store.Data;
using Store.UI.POS.Managers;

namespace Store.UI.POS.ViewModels;

public class LoginViewModel : ViewModelBase, IRoutableViewModel
{
    public LoginViewModel()
    {
        HostScreen = Locator.Current.GetService<IScreen>()!;
        LoginCommand = ReactiveCommand.Create(Login);
    }

    [Reactive]
    public string? Username { get; set; }

    [Reactive]
    public string? Password { get; set; }

    [Reactive]
    public bool IsLoggedIn { get; set; }

    public ICommand LoginCommand { get; set; }
    public string UrlPathSegment => "login";

    public IScreen HostScreen { get; set; }

    private void Login()
    {
        IsLoggedIn = true;

        UserManager.CurrentUser = new User
        {
            Name = "Mario Gabriell"
        };

        MessageBus.Current.SendMessage("Sales", "GoSales");
    }
}