using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IRecipeIngredientRepository : IBaseRepository<App.DAL.DTO.RecipeIngredient>
{
    Task RemoveByRecipeIdAsync(Guid recipeId);
    Task<IEnumerable<App.DAL.DTO.RecipeIngredient>> GetByRecipeIdAsync(Guid recipeId);
}