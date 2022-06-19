using ARS.Common.Bases;

namespace ARS.Module.Fitness.Models;

public class FitnessMessage : BaseData
{
    public int From { get; set; }
    public string Content { get; set; }
}
