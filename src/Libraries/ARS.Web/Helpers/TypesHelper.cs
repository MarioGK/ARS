namespace ARS.Web.Helpers;

public static class TypesHelper
{
    public static List<Type> AllTypes { get; }
    
    static TypesHelper()
    {
        AllTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).ToList();
    }
}