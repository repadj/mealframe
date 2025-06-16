using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Contracts;

namespace APP.BLL.DTO;

public class MealEntry : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Amount), ResourceType = typeof(App.Resources.Domain.MealEntry))]
    public decimal Amount { get; set; }
    
    [Display(Name = nameof(Unit), ResourceType = typeof(App.Resources.Domain.MealEntry))]
    public EUnit Unit { get; set; }
    
    [Display(Name = nameof(MealType), ResourceType = typeof(App.Resources.Domain.MealEntry))]
    public EMealType MealType { get; set; }
    
    public Guid? RecipeId { get; set; }
    
    [Display(Name = nameof(Recipe), ResourceType = typeof(App.Resources.Domain.MealEntry))]
    public Recipe? Recipe { get; set; }
    
    public Guid? ProductId { get; set; }
    
    [Display(Name = nameof(Product), ResourceType = typeof(App.Resources.Domain.MealEntry))]
    public Product? Product { get; set; }
    
    public Guid MealPlanId { get; set; }
    
    [Display(Name = nameof(MealPlan), ResourceType = typeof(App.Resources.Domain.MealEntry))]
    public MealPlan? MealPlan { get; set; }
}