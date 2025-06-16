using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DAL.DTO;

public class MealPlan : IDomainId
{
    public Guid Id { get; set; }
    
    public string PlanName { get; set; } = default!;
    
    public DateOnly Date { get; set; }
    
    public ICollection<MealEntry>? MealEntries { get; set; }
}