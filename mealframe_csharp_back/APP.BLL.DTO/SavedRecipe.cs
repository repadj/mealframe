using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace APP.BLL.DTO;

public class SavedRecipe : IDomainId
{
    public Guid Id { get; set; }
    
    public Guid RecipeId { get; set; }
    
    [Display(Name = nameof(Recipe), ResourceType = typeof(App.Resources.Domain.SavedRecipe))]
    public Recipe? Recipe { get; set; }
}