using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser<AppUserRole>
{
    [MinLength(1)]
    [MaxLength(32)] 
    public string Firstname { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(32)] 
    public string Lastname { get; set; } = default!;
    
    public ICollection<Recipe>? Recipes { get; set; }
    public ICollection<Product>? Products { get; set; }
    public ICollection<MealPlan>? MealPlans { get; set; }
    public ICollection<SavedRecipe>? SavedRecipes { get; set; }
    public ICollection<NutritionalGoal>? NutritionalGoals { get; set; }
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
}