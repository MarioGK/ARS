using ARS.Common.Bases;
using ARS.Modules.IOT.Enums;

namespace ARS.Modules.IOT.Models;

public class IOTDevice : BaseData
{
    public string ClientId { get; set; }= null!;
    public IOTType Type { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; }= null!;
}