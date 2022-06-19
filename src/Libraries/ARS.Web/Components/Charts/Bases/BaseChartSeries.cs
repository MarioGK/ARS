using ARS.Common.Helpers;
using ARS.Web.Components.Charts.Models;

namespace ARS.Web.Components.Charts.Bases;

public class BaseChartSeries : BaseChart
{
    [Parameter]
    [EditorRequired]
    public List<Series> Series { get; set; }

    protected string[] Labels = Array.Empty<string>();
    
    private void SetValues()
    {
        for (var i = 0; i < Series.Count; i++)
        {
            //If already has value skip
            if (Series[i].Color.IsNotNullOrEmpty())
            {
                continue;
            }
            
            Series[i].Color = GetColor(i);
        }
        
        ItemColors = Series.Select(x => x.Color).ToArray();
        
        Labels = Series.Select(x => x.Label).ToArray();
    }
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetValues();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        SetValues();
    }
}