using ARS.Module.Fitness.Components;
using ARS.Web.Interfaces;

namespace ARS.Module.Fitness;

public class FitnessModule : IModule
{
    public string Name => "Fitness Module";
    public Type NavMenu => typeof(FitnessMenu);
    
    public void Initialize()
    {
    }
}