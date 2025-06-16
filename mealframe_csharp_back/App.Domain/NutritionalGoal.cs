using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class NutritionalGoal : BaseEntityUser<AppUser>
{
    [Display(Name = nameof(CalorieTarget), ResourceType = typeof(App.Resources.Domain.NutritionalGoal))]
    public int CalorieTarget { get; set; }
    
    [Display(Name = nameof(ProteinTarget), ResourceType = typeof(App.Resources.Domain.NutritionalGoal))]
    public decimal ProteinTarget { get; set; }
    
    [Display(Name = nameof(FatTarget), ResourceType = typeof(App.Resources.Domain.NutritionalGoal))]
    public decimal FatTarget { get; set; }
    
    [Display(Name = nameof(SugarTarget), ResourceType = typeof(App.Resources.Domain.NutritionalGoal))]
    public decimal SugarTarget { get; set; }
    
    [Display(Name = nameof(CarbsTarget), ResourceType = typeof(App.Resources.Domain.NutritionalGoal))]
    public decimal CarbsTarget { get; set; }
    
    [Display(Name = nameof(SaltTarget), ResourceType = typeof(App.Resources.Domain.NutritionalGoal))]
    public decimal SaltTarget { get; set; }
}