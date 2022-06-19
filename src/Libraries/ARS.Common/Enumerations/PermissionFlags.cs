namespace ARS.Common.Enumerations;

[Flags]
public enum PermissionFlags
{
    Read = 1,
    Write = 2,
    Delete = 4,
    Full = 8
}