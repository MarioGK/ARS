namespace ARS.Modules.IOT.Models;

public class DataLog<T>
{
    public T Data { get; set; }
    public DateTime DateTime { get; set; }

    public DataLog(T data)
    {
        Data = data;
        DateTime = DateTime.UtcNow;
    }
}