using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ARS.Common.Attributes;
using ARS.Common.Bases;
using ARS.Common.Interfaces.DataTypes;


namespace ARS.Store.Common.Data;

public class Product : BaseData, ISearchable, ISelectable
{
    [AutoOptions("Nome", 0)]
    public string Name { get; set; } = null!;
    
    [AutoOptions("Descrição", 1)]
    public string? Description { get; set; }

    [Required]
    [AutoOptions("Preço", 2)]
    public decimal Price { get; set; }

    [Required]
    [AutoOptions("Quantidade", 3)]
    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public string? Image { get; set; }

    [JsonIgnore]
    public bool Selected { get; set; }

    public string SearchString => $"{Name} {Description}";
}