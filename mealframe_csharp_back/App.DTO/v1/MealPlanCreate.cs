using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class MealPlanCreate
{
    [Required]
    [MaxLength(128)]
    public string PlanName { get; set; } = default!;
    
    public DateOnly Date { get; set; }
    
    public List<MealEntryCreate>? MealEntries { get; set; }
}