using System.Reflection;
using ARS.Module.Fitness;
using ARS.Modules.Sales;
using ARS.Web.Interfaces;

namespace ARS.AdminPanel;

public class ModuleHandler
{
    public static void Initialize()
    {
        Modules = new List<IModule>
        {
            new SalesModule(),
            new FitnessModule(),
        };

        foreach (var module in Modules)
        {
            try
            {
                module.Initialize();
            }
            catch (Exception e)
            {
                //TODO proper logging
                Console.WriteLine(e);
            }
        }
        

        ExtraAssemblies = Modules.Select(x => x.GetType().Assembly).ToList();
    }

    public static List<IModule> Modules = new();

    public static List<Assembly> ExtraAssemblies = new();
}