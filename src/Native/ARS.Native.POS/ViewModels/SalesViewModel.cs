using System.Collections.ObjectModel;
using Store.UI.POS.Managers;

namespace Store.UI.POS.ViewModels;

public class SalesViewModel : ViewModelBase, IRoutableViewModel
{
    public SalesViewModel()
    {
        MessageBus.Current.Listen<ProductQuantityChangedMessage>().Subscribe(_ => NotifyChange());
        MessageBus.Current.Listen<OnCodeRead>("OnCodeRead").Subscribe(read => AddProduct(read.Code));

        HostScreen = Locator.Current.GetService<IScreen>()!;
        ConsultProductCommand = ReactiveCommand.Create(ConsultProduct);
        AddManualProductCommand = ReactiveCommand.Create(AddManualProduct);
        Logout = ReactiveCommand.Create(() => MessageBus.Current.SendMessage("Logout", "GoLogin"));

        this.WhenAnyValue(x => x.ManualProductCode).Throttle(TimeSpan.FromMilliseconds(200)).Subscribe(code => { });
    }

    public string UserName => UserManager.CurrentUser.Name;

    [Reactive]
    public string? ManualProductCode { get; set; }

    public ICommand ConsultProductCommand { get; set; }
    public ICommand AddManualProductCommand { get; set; }
    public ICommand Logout { get; set; }

    public int TotalUniqueProducts => Products.Count;
    public int TotalProducts => Products.Sum(x => x.Quantity);
    public double TotalPrice => Products.Sum(x => x.TotalPrice);

    public string TotalPriceFormatted => $"Total R$ {TotalPrice:N2}";

    public ObservableCollection<ProductModel> Products { get; } = new()
    {
        new ProductModel {Code = "2019022000100451", Name = "Orange PI", Price = 100}
    };

    public ObservableCollection<ProductModel> DataProducts { get; set; } = new()
    {
        new ProductModel {Code = "2019022000100451", Name = "Orange PI", Price = 100},
        new ProductModel {Code = "7898021460513", Name = "Papel", Price = 10},
        new ProductModel {Code = "4006381136389", Name = "Caneta Azul", Price = 10},
        new ProductModel {Code = "6956446900012", Name = "Caneta Vermelha", Price = 10}
    };

    public string? UrlPathSegment => "sales";
    public IScreen HostScreen { get; set; }

    public void AddManualProduct()
    {
        if (ManualProductCode == null)
        {
            return;
        }

        AddProduct(ManualProductCode);
        ManualProductCode = string.Empty;
    }

    public void AddProduct(string code)
    {
        var product = DataProducts.FirstOrDefault(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        if (product != null)
        {
            if (Products.Contains(product))
            {
                product.Quantity++;
            }
            else
            {
                Products.Add(product);
            }
        }

        NotifyChange();
    }

    public void ConsultProduct()
    {
        //DialogHost.DialogHost.Show(new ConfirmationDialogViewModel());
        //DialogHost.DialogHost.Show(new DialogViewModel(), "MainDialogHost");
        DialogHost.DialogHost.Show(new ConsultViewModel(), "MainDialogHost");
    }

    private void NotifyChange()
    {
        this.RaisePropertyChanged(nameof(TotalUniqueProducts));
        this.RaisePropertyChanged(nameof(TotalProducts));
        this.RaisePropertyChanged(nameof(TotalPriceFormatted));
    }

    public void Remove(ProductModel productModel)
    {
        Products.Remove(productModel);
        NotifyChange();
    }
}