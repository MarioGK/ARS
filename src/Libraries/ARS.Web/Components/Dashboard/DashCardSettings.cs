using MudBlazor;

namespace ARS.Web.Components.Dashboard;

public class DashCardSettings
{
    [Parameter]
    public int Size { get; set; } = 3;
    [Parameter]
    public string Title { get; set; }
    [Parameter]
    public string Icon { get; set; }
    [Parameter]
    public Color Color { get; set; }
    [Parameter]
    public Func<string> GetData { get; set; }
}