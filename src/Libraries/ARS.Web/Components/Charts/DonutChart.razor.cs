using System.ComponentModel;
using ARS.Common.Helpers;
using ARS.Web.Components.Charts.Bases;
using ARS.Web.Components.Charts.Svg;
using MudBlazor;
using MudBlazor.Charts.SVG.Models;

namespace ARS.Web.Components.Charts;

public partial class DonutChart : BaseChartItems
{
    private readonly List<CircleSVG> _circles = new();
    private readonly List<LegendSVG> _legends = new();
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        _circles.Clear();
        _legends.Clear();
        const double counterClockwiseOffset = 25;
        var totalPercent = 0d;

        var counter = 0;
        foreach (var data in NormalizedValues)
        {
            var labels = "";
            if (counter < Labels.Length)
            {
                labels = Labels[counter];
            }
            var legend = new LegendSVG
            {
                Index = counter,
                Labels = labels,
                Data = data.ToS()
            };
            _legends.Add(legend);
            
            var percent = data * 100;
            var reversePercent = 100 - percent;
            var offset = 100 - totalPercent + counterClockwiseOffset;
            totalPercent += percent;

            var circle = new CircleSVG
            {
                Color = ItemColors[counter],
                Index = counter,
                Radius = 15.91549430918954,
                StrokeDashArray = $"{percent.ToS()} {reversePercent.ToS()}",
                StrokeDashOffset = offset,
                Value = Values[counter],
                Label = labels
            };
            _circles.Add(circle);
            
            counter += 1;
        }
    }
}