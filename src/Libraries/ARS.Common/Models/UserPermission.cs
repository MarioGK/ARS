using ARS.Common.Bases;

namespace ARS.Common.Models;

public class UserPermission : BaseData
{
    public List<BaseModulePermission> Permissions { get; set; } = new();
}