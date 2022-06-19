namespace Store.UI.POS;

public class ViewLocator : IDataTemplate
{
    public IControl Build(object data)
    {
        var name = data.GetType().FullName!.Replace("ViewModel", "View");

        var type = Type.GetType(name);
        //Resolve Views from Store.UI
        if (!name.Contains("POS") && type == null)
        {
            type = App.StoreUI.GetType(name);
        }

        if (type != null)
        {
            return (Control) Activator.CreateInstance(type)!;
        }

        return new TextBlock {Text = "View Not Found: " + name};
    }

    public bool Match(object data)
    {
        return data is ViewModelBase;
    }
}