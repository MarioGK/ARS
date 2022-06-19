using ARS.Common.Helpers;
using ARS.Web.Components.Charts.Models;

namespace ARS.Web.Components.Charts.Bases;

public class BaseChartItems : BaseChart
{
    [Parameter]
    [EditorRequired]
    public List<ChartItem> Items { get; set; }

    protected double[] Values = Array.Empty<double>();
    
    protected double[] NormalizedValues= Array.Empty<double>();
    
    private void SetValues()
    {
        NormalizedValues = GetNormalizedData();

        for (var i = 0; i < Items.Count; i++)
        {
            //If already has value skip
            if (Items[i].Color.IsNotNullOrEmpty())
            {
                continue;
            }
            
            Items[i].Color = GetColor(i);
        }
        
        ItemColors = Items.Select(x => x.Color).ToArray();
        Values = Items.Select(x => x.Value).ToArray();
        Labels = Items.Select(x => x.Label).ToArray();
    }

    /// <summary>
    /// Scales the input data to the range between 0 and 1
    /// </summary>
    private double[] GetNormalizedData()
    {
        var total = Values.Sum();
        return Values.Select(x => Math.Abs(x) / total).ToArray();
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