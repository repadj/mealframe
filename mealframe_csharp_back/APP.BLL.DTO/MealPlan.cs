using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace APP.BLL.DTO;

public class MealPlan : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(32)] 
    [Display(Name = nameof(PlanName), ResourceType = typeof(App.Resources.Domain.MealPlan))]
    public string PlanName { get; set; } = default!;
    
    public DateOnly Date { get; set; }
    
    public ICollection<MealEntry>? MealEntries { get; set; }
}