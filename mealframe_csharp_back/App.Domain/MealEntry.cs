using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class MealEntry : BaseEntity
{
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