namespace ARS.Common.Helpers;

public static class ListExtensions
{
    public static IEnumerable<List<object>> PartitionByTypes(this List<object> values)
    {
        var count = 0;
        return values.Select((o, i) => new
            {
                Object = o,
                Group = i == 0 || o.GetType() == values[i - 1].GetType() ? count : ++count
            })
            .GroupBy(x => x.Group)
            .Select(g => g.Select(x => x.Object).ToList());
    }
}