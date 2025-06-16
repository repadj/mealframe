using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class SavedRecipeMapper : IMapper<App.DAL.DTO.SavedRecipe, App.Domain.SavedRecipe>
{
    public SavedRecipe? Map(Domain.SavedRecipe? entity)
    {
        if (entity == null) return null;
        var res = new SavedRecipe()
        {
            Id = entity.Id,
            RecipeId = entity.RecipeId
        };
        return res;
    }

    public Domain.SavedRecipe? Map(SavedRecipe? entity)
    {
        if (entity == null) return null;
        var res = new Domain.SavedRecipe()
        {
            Id = entity.Id,
            RecipeId = entity.RecipeId
        };
        return res;
    }
}