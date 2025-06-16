using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class Recipe : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(128)] 
    public string RecipeName { get; set; } = default!;
    
    [Required]
    [MaxLength(128)] 
    public string Description { get; set; } = default!;
    
    [Required]
    public int CookingTime { get; set; }
    
    [Required]
    public int Servings { get; set; }
    
    [Required]
    public string PictureUrl { get; set; } = default!;
    
    public bool Public { get; set; }
    
    public List<RecipeIngredientDetail> Ingredients { get; set; } = new();
}