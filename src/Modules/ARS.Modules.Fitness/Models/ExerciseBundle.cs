

namespace ARS.Module.Fitness.Models;

public class ExerciseBundle
{
    public string Objective { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public List<ExerciseRoutine>? Routines { get; set; }
}