

namespace ARS.Common.Bases;

public class BaseData
{
    public BaseData()
    {
        CreatedAt = ModifiedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; set; }

    public DateTime ModifiedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public BaseData? Clone()
    {
        return MemberwiseClone() as BaseData;
    }
    
    public T? Clone<T>() where T : BaseData
    {
        return MemberwiseClone() as T;
    }
}