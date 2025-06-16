using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Recipe : BaseEntityUser<AppUser>
{
    [MaxLength(128)] 
    [Display(Name = nameof(RecipeName), ResourceType = typeof(App.Resources.Domain.Recipe))]
    public string RecipeName { get; set; } = default!;
    
    [MaxLength(128)] 
    [Display(Name = nameof(Description), ResourceType = typeof(App.Resources.Domain.Recipe))]
    public string Description { get; set; } = default!;
    
    [Display(Name = nameof(CookingTime), ResourceType = typeof(App.Resources.Domain.Recipe))]
    public int CookingTime { get; set; }
    
    public int Servings { get; set; }
    
    public string PictureUrl { get; set; } = default!;
    
    [Display(Name = nameof(Public), ResourceType = typeof(App.Resources.Domain.Recipe))]
    public bool Public { get; set; }
    
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    public ICollection<SavedRecipe>? SavedRecipes { get; set; }
    
}