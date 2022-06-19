using MudBlazor;

namespace ARS.Web.Components.Dialogs;

public partial class AutoDialog<T> : IDisposable where T : BaseData
{
    public AutoDialog()
    {
    }
    
    [CascadingParameter]
    public MudDialogInstance? MudDialog { get; set; }

    private void Cancel()
    {
        Dispose();
        MudDialog?.Close();
    }

    public override async Task OnValidSubmit()
    {
        await base.OnValidSubmit();
        MudDialog?.Close();
    }
}