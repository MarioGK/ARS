using ARS.Common.Bases;
using ARS.Store.Common.Data;
using LiteDB.Async;


namespace ARS.Store.Common.Collections;

public class ProductCollection : BaseCollection<Product>
{
    public ProductCollection(ILiteDatabaseAsync db) : base(db)
    {
    }
}