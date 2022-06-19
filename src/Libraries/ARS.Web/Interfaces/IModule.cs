namespace ARS.Web.Interfaces;

public interface IModule
{
    string Name { get; }

    Type NavMenu { get; }
    
    void Initialize();
}