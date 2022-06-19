namespace ARS.Common.Configs;

public class PosConfig : BaseConfig
{
    public string StoreName { get; set; }
    public string ComputerName { get; set; }

    public List<APIConfig> Apis { get; set; } = new();
}