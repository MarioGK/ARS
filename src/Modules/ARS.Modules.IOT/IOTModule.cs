using ARS.Modules.IOT.Components;
using ARS.Web.Interfaces;

namespace ARS.Modules.IOT;

public class IOTModule : IModule
{
    public string Name => "IOT Module";
    public Type NavMenu => typeof(IOTMenu);
    
    public void Initialize()
    {
    }
}