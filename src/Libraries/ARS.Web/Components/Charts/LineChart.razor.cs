using ARS.Common.Helpers;
using ARS.Web.Components.Charts.Bases;
using ARS.Web.Components.Charts.Svg;
using MudBlazor;
using MudBlazor.Charts.SVG.Models;
using MudBlazor.Components.Chart;
using MudBlazor.Components.Chart.Interpolation;

namespace ARS.Web.Components.Charts;

public partial class LineChart : BaseChartSeries
{
    private const int MaxHorizontalGridLines = 100;

    private List<PathSVG> _horizontalLines = new();
    private List<TextSVG> _horizontalValues = new();

    private List<PathSVG> _verticalLines = new();
    private List<TextSVG> _verticalValues = new();
    
    private List<PathSVG> _chartLines = new();

    private List<LegendSVG> _legends = new();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _horizontalLines.Clear();
        _verticalLines.Clear();
        _horizontalValues.Clear();
        _verticalValues.Clear();
        _legends.Clear();
        _chartLines.Clear();
        
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

        var gridYUnits =  20d;
        if (gridYUnits <= 0)
        {
            gridYUnits = 20;
        }
        var maxYTicks = 100;
        double gridXUnits = 30;

        var numVerticalLines = numValues - 1;

        var numHorizontalLines = (int) (maxY / gridYUnits) + 1;

        // this is a safeguard against millions of gridlines which might arise with very high values
        while (numHorizontalLines > maxYTicks)
        {
            gridYUnits *= 2;
            numHorizontalLines = (int) (maxY / gridYUnits) + 1;
        }

        var verticalStartSpace = 25.0;
        var horizontalStartSpace = 30.0;
        var verticalEndSpace = 25.0;
        var horizontalEndSpace = 30.0;

        var verticalSpace = (boundHeight - verticalStartSpace - verticalEndSpace) / numHorizontalLines;
        var horizontalSpace = (boundWidth - horizontalStartSpace - horizontalEndSpace) / numVerticalLines;
        var interpolationOption = InterpolationOption.Straight;

        //Horizontal Grid Lines
        var y = verticalStartSpace;
        double startGridY = 0;
        for (var counter = 0; counter <= numHorizontalLines; counter++)
        {
            var line = new PathSVG
            {
                Index = counter,
                Data =
                    $"M {horizontalStartSpace.ToS()} {(boundHeight - y).ToS()} L {(boundWidth - horizontalEndSpace).ToS()} {(boundHeight - y).ToS()}"
            };
            _horizontalLines.Add(line);

            var lineValue = new TextSVG
            {
                Index = counter,
                X = horizontalStartSpace - 10, Y = boundHeight - y + 5,
                Value = startGridY.ToS(/*MudChartParent?.ChartOptions.YAxisFormat*/)
            };
            _horizontalValues.Add(lineValue);

            startGridY += gridYUnits;
            y += verticalSpace;
        }

        //Vertical Grid Lines
        var x = horizontalStartSpace;
        double startGridX = 0;
        for (var counter = 0; counter <= numVerticalLines; counter++)
        {

            var line = new PathSVG
            {
                Index = counter,
                Data = $"M {x.ToS()} {(boundHeight - verticalStartSpace).ToS()} L {x.ToS()} {verticalEndSpace.ToS()}"
            };
            _verticalLines.Add(line);

            var xLabels = "X Label";
            if (counter < numXLabels)
            {
                //xLabels = XAxisLabels[counter];
            }

            var lineValue = new TextSVG
            {
                Index = counter,
                X = x, 
                Y = boundHeight - 2, 
                Value = xLabels
            };
            _verticalValues.Add(lineValue);

            startGridX += gridXUnits;
            x += horizontalSpace;
        }


        //Chart Lines
        var colorcounter = 0;
        foreach (var item in Series)
        {
            var chartLine = "";
            double gridValueX = 0;
            double gridValueY = 0;
            var firstTime = true;
            double[] XValues = new double[item.Values.Count];
            double[] YValues = new double[item.Values.Count];
            ILineInterpolator interpolator;
            for (var i = 0; i <= item.Values.Count - 1; i++)
            {
                if (i == 0)
                    XValues[i] = 30;
                else
                    XValues[i] = XValues[i - 1] + horizontalSpace;

                var gridValue = item.Values[i] * verticalSpace / gridYUnits;
                YValues[i] = boundHeight - (verticalStartSpace + gridValue);

            }

            switch (interpolationOption)
            {
                case InterpolationOption.NaturalSpline:
                    interpolator = new NaturalSpline(XValues, YValues);
                    break;
                case InterpolationOption.EndSlope:
                    interpolator = new EndSlopeSpline(XValues, YValues);
                    break;
                case InterpolationOption.Periodic:
                    interpolator = new PeriodicSpline(XValues, YValues);
                    break;
                case InterpolationOption.Straight:
                default:
                    interpolator = new NoInterpolation();
                    break;
            }

            if (interpolator?.InterpolationRequired == true)
            {
                horizontalSpace = (boundWidth - horizontalStartSpace - horizontalEndSpace) /
                                  interpolator.InterpolatedXs.Length;
                foreach (var yValue in interpolator.InterpolatedYs)
                {

                    if (firstTime)
                    {

                        chartLine += "M ";
                        firstTime = false;
                        gridValueX = horizontalStartSpace;
                        gridValueY = verticalStartSpace;
                    }
                    else
                    {
                        chartLine += " L ";
                        gridValueX += horizontalSpace;
                        gridValueY = verticalStartSpace;
                    }

                    gridValueY = yValue;
                    //chartLine = chartLine + ToS(gridValueX) + " " + ToS(gridValueY);
                    chartLine = $"{chartLine}{gridValueX.ToS()} {gridValueY.ToS()}";
                }
            }
            else
            {
                foreach (var dataLine in item.Values)
                {
                    if (firstTime)
                    {
                        chartLine += "M ";
                        firstTime = false;
                        gridValueX = horizontalStartSpace;
                        gridValueY = verticalStartSpace;
                    }
                    else
                    {
                        chartLine += " L ";
                        gridValueX += horizontalSpace;
                        gridValueY = verticalStartSpace;
                    }

                    var gridValue = dataLine * verticalSpace / gridYUnits;
                    gridValueY = boundHeight - (gridValueY + gridValue);
                    //chartLine = chartLine + ToS(gridValueX) + " " + ToS(gridValueY);
                    chartLine = $"{chartLine}{gridValueX.ToS()} {gridValueY.ToS()}";
                }
            }

            var line = new PathSVG
            {
                Index = colorcounter,
                Data = chartLine
            };

            var legend = new LegendSVG
            {
                Index = colorcounter,
                Labels = item.Label
            };
            
            colorcounter++;
            
            _chartLines.Add(line);
            _legends.Add(legend);
        }
    }
}