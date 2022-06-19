using ARS.Modules.Sales.Components;

using ARS.Web.Interfaces;

namespace ARS.Modules.Sales;

public class SalesModule : IModule
{
    public string Name => "SalesModule";
    public Type NavMenu => typeof(SalesMenu);
    public void Initialize()
    {
        
    }
}