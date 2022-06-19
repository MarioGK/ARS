using ARS.Common.Bases;

namespace ARS.Store.Common.Data;

public class Quote : BaseData
{
    public int CustomerId { get; set; }
    public string Author { get; set; }
    public List<Product> Products { get; set; }
}