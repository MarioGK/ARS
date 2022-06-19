using ARS.Common.Attributes;

namespace ARS.Common.Bases;

public class BaseType : BaseData
{
    [AutoOptions("Nome", 0)]
    public string Name { get; set; } = null!;
}