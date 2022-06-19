using ARS.Common.Bases;

namespace ARS.Store.Common.Data;

public class Sale : BaseData
{
    public List<Product> Products { get; set; }

    public decimal TotalPrice { get; set; }
}