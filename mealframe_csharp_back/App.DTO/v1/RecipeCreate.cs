using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class RecipeCreate
{
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
    
    [Required]
    [MinLength(1, ErrorMessage = "At least one ingredient is required.")]
    public List<RecipeIngredientCreate> Ingredients { get; set; } = new();
}