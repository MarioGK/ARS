using ARS.Common.Bases;
using ARS.Common.Models;
using LiteDB.Async;


namespace ARS.Common.Collections;

public class UserCollection : BaseCollection<User>
{
    public UserCollection(ILiteDatabaseAsync db) : base(db)
    {
    }
}