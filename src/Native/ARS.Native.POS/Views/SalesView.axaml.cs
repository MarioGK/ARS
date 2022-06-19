namespace Store.UI.POS.Views;

public class SalesView : ReactiveUserControl<SalesViewModel>
{
    public SalesView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}