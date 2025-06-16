using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class SavedRecipeMapper : IMapper<APP.BLL.DTO.SavedRecipe, App.DAL.DTO.SavedRecipe>
{
    public SavedRecipe? Map(DAL.DTO.SavedRecipe? entity)
    {
        if (entity == null) return null;
        var res = new SavedRecipe()
        {
            Id = entity.Id,
            RecipeId = entity.RecipeId
        };
        return res;
    }

    public DAL.DTO.SavedRecipe? Map(SavedRecipe? entity)
    {
        if (entity == null) return null;
        var res = new DAL.DTO.SavedRecipe()
        {
            Id = entity.Id,
            RecipeId = entity.RecipeId
        };
        return res;
    }
}