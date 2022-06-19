namespace ARS.Web.Components;

public partial class AutoForm<T> where T : BaseData
{
    [Parameter]
    public string Class { get; set; } = "";
    
    [Parameter]
    public string? Title { get; set; }
}