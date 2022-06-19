using ARS.Common.Enumerations;

namespace ARS.Common.Bases;

public class BaseModulePermission
{
    public Modules Module { get; set; }
    public PermissionFlags Permission { get; set; }
}