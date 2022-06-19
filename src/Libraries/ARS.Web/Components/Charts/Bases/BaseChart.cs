using ARS.Common.Helpers;
using ARS.Web.Components.Charts.Parts;
using ARS.Web.Components.Charts.Svg;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MudBlazor.Extensions;
using MudBlazor.Utilities;

namespace ARS.Web.Components.Charts.Bases;

public abstract class BaseChart : ComponentBase
{
    protected string[] ItemColors = Array.Empty<string>();
    
    public string[] ChartPalette { get; set; } =
    {
        Colors.Blue.Accent3,
        Colors.Teal.Accent3, 
        Colors.Amber.Accent3, Colors.Orange.Accent3, Colors.Red.Accent3, Colors.DeepPurple.Accent3, Colors.Green.Accent3, Colors.LightBlue.Accent3, Colors.Teal.Lighten1, Colors.Amber.Lighten1, Colors.Orange.Lighten1, Colors.Red.Lighten1, Colors.DeepPurple.Lighten1, Colors.Green.Lighten1, Colors.LightBlue.Lighten1, Colors.Amber.Darken2, Colors.Orange.Darken2, Colors.Red.Darken2, Colors.DeepPurple.Darken2, Colors.Grey.Darken2
    };

    public bool TooltipVisible { get; private set; }

    [Parameter]
    public bool ToolTipEnabled { get; set; } = true;
    
    public int HoverIndex { get; private set; }

    private int _tooltipX;
    private int _tooltipY;
    private double _tooltipValue;
    private string _tooltipLabel;
    private int _lastHoverRenderIndex = -1;
    
    protected string[] Labels = Array.Empty<string>();

    protected string GetColor(int index) => ChartPalette[index % ChartPalette.Length];
    
    protected RenderFragment Tooltip;

    protected void OnMouseHover(MouseEventArgs args, BaseSvg svgPath)
    {
        //Set the index of the item that is hovered
        HoverIndex = svgPath.Index;
        //Set the tooltip position and make it visible
        TooltipVisible = true;
        //This is only needed for the tooltip not derp around on the first render
        SetTooltipPosition(args);
        _tooltipValue = svgPath.Value;
        _tooltipLabel = svgPath.Label;

        //Only render the tooltip if the index has changed
        if (_lastHoverRenderIndex == HoverIndex)
        {
            return;
        }

        //Updates the last rendered index
        _lastHoverRenderIndex = HoverIndex;
        //Render the tooltip
        Tooltip = RenderTooltip();
    }
    
    protected void OnMouseOut(MouseEventArgs args, BaseSvg svgPath)
    {
        //Hide the tooltip
        TooltipVisible = false;
    }
    
    protected void OnMouseMove(MouseEventArgs args, BaseSvg svgPath)
    {
        SetTooltipPosition(args);
    }

    private void SetTooltipPosition(MouseEventArgs args)
    {
        var x = (int)args.PageX;
        var y = (int)args.PageY;
            
        //So we dont render anything new if the position is the same
        if(_tooltipX == x && _tooltipY == y)
        {
            return;
        }
            
        _tooltipX = x;
        _tooltipY = y;
        
        //Console.WriteLine($"x: {x}, y: {y}");
    }

    [Parameter]
    public string Class { get; set; }
    
    [Parameter]
    public string Width { get; set; } = "100%";
    
    [Parameter]
    public string Height { get; set; } = "80%";
    
    public Position LegendPosition { get; set; } = Position.Bottom;
    
    private int _selectedIndex;

    /// <summary>
    /// Selected index of a portion of the chart.
    /// </summary>
    [Parameter]
    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            if (value != _selectedIndex)
            {
                _selectedIndex = value;
                SelectedIndexChanged.InvokeAsync(value);
            }
        }
    }

    [Parameter] 
    public EventCallback<int> SelectedIndexChanged { get; set; }
    
    [Parameter]
    public string Style { get; set; }
    
    /// <summary>
    /// This methods is called when the mouse is over the chart item, and renders the tooltip.
    /// </summary>
    /// <returns></returns>
    protected RenderFragment RenderTooltip()
    {
        //Default tooltip
        return builder =>
        {
            builder.OpenComponent<ChartTooltip>(0);
            builder.AddAttribute(1, nameof(ChartTooltip.X), _tooltipX);
            builder.AddAttribute(2, nameof(ChartTooltip.Y), _tooltipY);
            builder.AddAttribute(3, nameof(ChartTooltip.Label), _tooltipLabel);
            builder.AddAttribute(4, nameof(ChartTooltip.Value), _tooltipValue.ToS());
            builder.AddAttribute(4, nameof(ChartTooltip.Color), ItemColors[HoverIndex]);

            builder.CloseComponent();
        };
    }
    
    protected string Classname =>
        new CssBuilder("mud-chart")
            .AddClass($"mud-chart-legend-{LegendPosition.ToDescriptionString()}")
            .AddClass(Class)
            .Build();
}