using System.Reflection;

namespace ARS.Web.Interfaces;

public interface IBuildableComponent
{
    int Priority { get; }

    bool CanBuild(PropertyInfo propertyInfo);
}