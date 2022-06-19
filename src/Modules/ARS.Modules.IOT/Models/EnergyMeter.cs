namespace ARS.Modules.IOT.Models;

public class EnergyMeter : IOTDevice
{
    public List<DataLog<float>> Current { get; set; }
    public List<DataLog<float>> Voltage { get; set; }
}