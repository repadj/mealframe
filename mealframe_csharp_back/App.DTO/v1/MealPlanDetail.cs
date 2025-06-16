using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class MealPlanDetail : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string PlanName { get; set; } = default!;
    
    public DateOnly Date { get; set; }
    
    public List<MealEntryDetail> MealEntries { get; set; } = new();
    
}