using System.Text.Json.Serialization;
using App.Domain;

namespace App.DTO.v1;

public class MealEntryCreate
{
    public decimal Amount { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EUnit Unit { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EMealType MealType { get; set; }
    
    public Guid? RecipeId { get; set; }
    
    public Guid? ProductId { get; set; }
}