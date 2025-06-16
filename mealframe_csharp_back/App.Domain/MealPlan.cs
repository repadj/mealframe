using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class MealPlan : BaseEntityUser<AppUser>
{
    [MaxLength(32)] 
    [Display(Name = nameof(PlanName), ResourceType = typeof(App.Resources.Domain.MealPlan))]
    public string PlanName { get; set; } = default!;
    
    public DateOnly Date { get; set; }

    public ICollection<MealEntry>? MealEntries { get; set; }
}