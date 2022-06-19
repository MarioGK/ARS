using ARS.Common.Attributes;
using ARS.Common.Bases;
using ARS.Common.Interfaces.DataTypes;

namespace ARS.Module.Fitness.Models;

public class ExerciseTemplate : BaseData, ISearchable
{
    [AutoOptions("Nome", 0)]
    public string Name { get; set; }
    [AutoOptions("Tipo", 1)]
    public string Type { get; set; }
    [AutoOptions("Tempo de descanso", 2)]
    public string WaitingTime { get; set; }
    [AutoOptions("Descrição", 3)]
    public string? Description { get; set; }
    [AutoOptions("Series", 4)]
    public int Series { get; set; }
    [AutoOptions("Repetições", 5)]
    public int Repeticao { get; set; }

    public string SearchString => $"{Name} {Type} {Description}";
}