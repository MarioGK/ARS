using ARS.Common.Bases;
using ARS.Module.Fitness.Models;
using LiteDB.Async;


namespace ARS.Module.Fitness.Collections;

public class FitnessUserCollection : BaseCollection<FitnessUser>
{
    public FitnessUserCollection(ILiteDatabaseAsync db) : base(db)
    {
    }
}