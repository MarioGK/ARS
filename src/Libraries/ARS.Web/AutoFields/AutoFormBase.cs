using ARS.Common.Helpers;
using ARS.Web.Services;
using LiteDB.Async;

namespace ARS.Web.AutoFields;

public abstract class AutoFormBase<T> : ComponentBase, IDisposable where T : BaseData
{
    protected T Clone { get; private set; } = null!;
    public bool Loading { get; set; }
    
    [Parameter]
    [EditorRequired]
    public T Model { get; set; } = null!;
    
    public AutoFields<T>? AutoFields { get; set; }

    [Parameter]
    public AutoFieldOptions? Options { get; set; }
    
    [Inject]
    private AutoTableItemAddedService ItemAddedService { get; set; }= null!;
    [Inject]
    private ILiteDatabaseAsync Database { get; set; }= null!;

    public virtual async Task OnValidSubmit()
    {
        Console.WriteLine("Valid Submit");
        using var stopTimer = new StopTimer("AutoDialog.ValidSubmit");

        await Save();
    }

    protected override void OnInitialized()
    {
        Clone = Model.Clone<T>()!;
    }

    public virtual async Task Save()
    {
        try
        {
            if(Loading)
            {
                return;
            }
            Loading = true;
            //await Task.Delay(5000);
            var collection = new BaseCollection<T>(Database);
            await collection.Insert(Model);
            ItemAddedService.Publish(Model);
        }
        catch (Exception e)
        {
            return;
        }
    }

    public void Dispose()
    {
        Model = Clone;
    }
}