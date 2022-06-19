using ARS.Web.AutoFields;
using ARS.Web.Components.Dialogs;
using MudBlazor;

namespace ARS.Web.Services;

public class AutoDialogService
{
    private readonly IDialogService _dialogService;

    public AutoDialogService(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public IDialogReference Open<T>(T model, AutoFieldOptions? options = default) where T : BaseData
    {
        options ??= new AutoFieldOptions();

        var parameters = new DialogParameters
        {
            {"Model", model},
            {"Options", options}
        };

        if (string.IsNullOrWhiteSpace(options.Title))
        {
            options.Title = model?.GetType().Name ?? "null";
        }

        return _dialogService.Show<AutoDialog<T>>(options.Title, parameters);
    }

    public async Task<bool> ShowConfirmDialog()
    {
        var confirmDialog = _dialogService.Show<ConfimationDialog>("Confirmação");
        var dialogResult = await confirmDialog.Result;

        return !dialogResult.Cancelled;
    }
}