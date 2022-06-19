using ARS.Common.Attributes;
using ARS.Common.Bases;
using ARS.Common.Enumerations;
using ARS.Common.Interfaces.DataTypes;

namespace ARS.Common.Models;

[AutoChart<User>(Label = "Usuarios", Period = Period.Week)]
public class User : BaseData, ISearchable, IAutoChartable
{
    [AutoOptions("Nome", 0)]
    public string Name { get; set; } = null!;
    
    [AutoOptions("Login", 1)]
    public string Login { get; set; } = null!;
    
    [AutoOptions(true, false, "Senha", 2)]
    public string Password { get; set; } = null!;
    
    [AutoOptions("Email", 3)]
    public string? Email { get; set; }

    [AutoOptions(false)]
    public string Salt { get; set; } = null!;

    [AutoOptions(false, false, "Token", 4)]
    public Token? Token { get; set; } = new();

    [AutoOptions(false)]
    public List<UserPermission> Permissions { get; set; } = new();
    
    [AutoOptions(true, false, "Admin",2)]
    public bool Admin { get; set; } = false;

    public string SearchString => $"{Name} {Login} {Email}";
    
    public double GetAutoChartValue()
    {
        //TODO
        return 1;
    }
}