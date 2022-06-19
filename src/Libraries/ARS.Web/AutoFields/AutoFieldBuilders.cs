using ARS.Web.Helpers;
using ARS.Web.Interfaces;

namespace ARS.Web.AutoFields;

public class AutoFieldBuilders
{
    public static List<IBuildableComponent> BuildableComponents { get; private set; } = new();

    public static void Create()
    {
        var baseComponentType = typeof(IBuildableComponent);
        var allTypes = TypesHelper.AllTypes;
        Console.WriteLine($"Found {allTypes.Count} types in assemblies");

        var buildableComponentsTypes = allTypes
            .Where(x => baseComponentType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();

        Console.WriteLine($"Found {buildableComponentsTypes.Count} components");

        foreach (var type in buildableComponentsTypes)
            try
            {
                var instance = (IBuildableComponent) Activator.CreateInstance(type)!;
                Console.WriteLine("Created instance of " + type.Name);
                BuildableComponents.Add(instance);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        //Order builders 
        BuildableComponents = BuildableComponents.OrderBy(x => x.Priority).ToList();

        Console.WriteLine($"Found {BuildableComponents.Count} buildable components");
    }
}