using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DAL.DTO;

public class Recipe : IDomainId
{
    public Guid Id { get; set; }
    
    public string RecipeName { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public int CookingTime { get; set; }
    
    public int Servings { get; set; }
    
    public string PictureUrl { get; set; } = default!;
    public bool Public { get; set; }
    
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    public ICollection<SavedRecipe>? SavedRecipes { get; set; }
}