using ARS.Common.Attributes;
using ARS.Common.Bases;

namespace ARS.Store.Common.Data;

public class Customer : BaseData
{
    [AutoOptions("Nome", 0)]
    public string Name { get; set; }
    [AutoOptions("Email", 1)]
    public string Email { get; set; }
    [AutoOptions("Telefone", 2)]
    public string Phone { get; set; }
    [AutoOptions("Endereço", 3)]
    public string Address { get; set; }
    [AutoOptions("Cidade", 4)]
    public string City { get; set; }
    [AutoOptions("Estado", 5)]
    public string State { get; set; }
    [AutoOptions("CEP", 6)]
    public string ZipCode { get; set; }
    [AutoOptions("Status", 7)]
    public string Status { get; set; }
}