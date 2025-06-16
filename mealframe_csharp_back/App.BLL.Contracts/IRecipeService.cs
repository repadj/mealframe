using APP.BLL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IRecipeService : IBaseService<APP.BLL.DTO.Recipe>
{
    Task<APP.BLL.DTO.Recipe> CreateWithIngredientsAsync(APP.BLL.DTO.Recipe recipe, Guid userId);
    
    Task<Recipe?> GetDetailedAsync(Guid id, Guid userId);
    
    Task<Recipe> UpdateWithIngredientsAsync(Recipe recipe, Guid userId);

    Task RemoveRecipeWithIngredientsAsync(Guid id, Guid userId = default);

    Task<RecipeMacroSummary?> GetRecipeMacrosPerServingAsync(Guid recipeId, Guid userId);
}