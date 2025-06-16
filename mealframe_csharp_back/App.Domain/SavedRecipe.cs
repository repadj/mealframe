using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class SavedRecipe : BaseEntityUser<AppUser>
{ 
    public Guid RecipeId { get; set; }
    
    [Display(Name = nameof(Recipe), ResourceType = typeof(App.Resources.Domain.SavedRecipe))]
    public Recipe? Recipe { get; set; }
}