using ARS.Common.Bases;
using ARS.Common.Enumerations;

namespace ARS.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AutoChart<T> : Attribute where T : BaseData
{
    public string Label { get; set; }

    public Period Period { get; set; }
}