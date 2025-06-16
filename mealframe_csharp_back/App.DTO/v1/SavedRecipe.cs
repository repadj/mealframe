using Base.Contracts;

namespace App.DTO.v1;

public class SavedRecipe : IDomainId
{
    public Guid Id { get; set; }
    
    public Guid RecipeId { get; set; }
}