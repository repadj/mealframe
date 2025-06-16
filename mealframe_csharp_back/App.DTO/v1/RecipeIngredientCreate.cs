using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using App.Domain;

namespace App.DTO.v1;

public class RecipeIngredientCreate
{
    [Required]
    public decimal Amount { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EUnit Unit { get; set; }

    [Required]
    public Guid ProductId { get; set; }
}