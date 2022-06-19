using ARS.Common.Bases;
using ARS.Module.Fitness.Models;
using LiteDB.Async;


namespace ARS.Module.Fitness.Collections;

public class MessageCollection : BaseCollection<FitnessMessage>
{
    public MessageCollection(ILiteDatabaseAsync db) : base(db)
    {
    }
}