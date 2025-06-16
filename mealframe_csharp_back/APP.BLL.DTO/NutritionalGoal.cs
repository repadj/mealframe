using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace APP.BLL.DTO;

public class NutritionalGoal : IDomainId
{
    public Guid Id { get; set; }
    
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