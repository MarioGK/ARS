using ARS.Common.Bases;
using ARS.Module.Fitness.Models;
using LiteDB.Async;


namespace ARS.Module.Fitness.Collections;

public class ExerciseTemplateCollection : BaseCollection<ExerciseTemplate>
{
    public ExerciseTemplateCollection(ILiteDatabaseAsync db) : base(db)
    {
    }
}