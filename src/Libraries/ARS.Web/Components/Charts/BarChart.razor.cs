using ARS.Common.Helpers;
using ARS.Web.Components.Charts.Bases;
using ARS.Web.Components.Charts.Models;
using ARS.Web.Components.Charts.Svg;

namespace ARS.Web.Components.Charts;

public partial class BarChart : BaseChartSeries
{
    private List<PathSVG> _horizontalLines = new();
    private List<TextSVG> _horizontalValues = new();
    
    private List<PathSVG> _verticalLines = new();
    private List<TextSVG> _verticalValues = new();
    
    private List<PathSVG> _bars = new();

    protected override void OnParametersSet()
    {
         base.OnParametersSet();
            
            //This value is not coming from the parent component automatically
            
            _horizontalLines.Clear();
            _verticalLines.Clear();
            _horizontalValues.Clear();
            _verticalValues.Clear();
            _bars.Clear();
        

            var maxY = 0.0;
            var numValues = 0;
            var numXLabels = Labels.Length;
            
            foreach (var item in Series)
            {
                if (numValues < item.Values.Count)
                {
                    numValues = item.Values.Count;
                }

                maxY = item.Values.Prepend(maxY).Max();
            }

            var boundHeight = 350.0;
            var boundWidth = 650.0;
            
            double gridYUnits = 10;
            //double gridXUnits = 30;

            var numVerticalLines = numValues - 1;

            var numHorizontalLines = (int)Math.Ceiling(maxY / gridYUnits);

            const double verticalStartSpace = 25.0;
            const double horizontalStartSpace = 30.0;
            const double verticalEndSpace = 25.0;
            const double horizontalEndSpace = 30.0;

            var verticalSpace = (boundHeight - verticalStartSpace - verticalEndSpace) / numHorizontalLines;
            var horizontalSpace = (boundWidth - horizontalStartSpace - horizontalEndSpace) / numVerticalLines;

            //Horizontal Grid Lines
            var y = verticalStartSpace;
            double startGridY = 0;
            for (var counter = 0; counter <= numHorizontalLines; counter++)
            {
                var line = new PathSVG
                {
                    Index = counter,
                    Data = $"M {horizontalStartSpace.ToS()} {(boundHeight - y).ToS()} L {(boundWidth - horizontalEndSpace).ToS()} {(boundHeight - y).ToS()}"
                };
                _horizontalLines.Add(line);

                var lineValue = new TextSVG
                {
                    X = horizontalStartSpace - 10,
                    Y = boundHeight - y + 5, 
                    Value = startGridY.ToS(/*MudChartParent?.ChartOptions.YAxisFormat*/)
                };
                _horizontalValues.Add(lineValue);

                startGridY += gridYUnits;
                y += verticalSpace;
            }

            //Bars
            var colorcounter = 0;
            double barsPerSeries = 0;
            foreach (var item in Series)
            {
                var gridValueX = horizontalStartSpace + barsPerSeries;
                var gridValueY = boundHeight - verticalStartSpace;

                foreach (var dataLine in item.Values)
                {
                    var dataValue = dataLine * verticalSpace / gridYUnits;
                    var gridValue = gridValueY - dataValue;
                    var bar = $"M {gridValueX.ToS()} {gridValueY.ToS()} L {gridValueX.ToS()} {gridValue.ToS()}";

                    gridValueX += horizontalSpace;

                    var line = new PathSVG
                    {
                        Index = colorcounter,
                        Data = bar,
                        Value = dataLine,
                        Label = item.Label
                    };
                    _bars.Add(line);
                }

                barsPerSeries += 10;
                
                colorcounter++;
            }
    }
}