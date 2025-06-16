using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class SavedRecipeMapper : IMapper<SavedRecipe, APP.BLL.DTO.SavedRecipe>
{
    public SavedRecipe? Map(APP.BLL.DTO.SavedRecipe? entity)
    {
        if (entity == null) return null;
        var res = new SavedRecipe()
        {
            Id = entity.Id,
            RecipeId = entity.RecipeId
        };
        return res;
    }

    public APP.BLL.DTO.SavedRecipe? Map(SavedRecipe? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.SavedRecipe()
        {
            Id = entity.Id,
            RecipeId = entity.RecipeId
        };
        return res;
    }

    public APP.BLL.DTO.SavedRecipe Map(SavedRecipeCreate entity)
    {
        var res = new APP.BLL.DTO.SavedRecipe()
        {
            Id = Guid.NewGuid(),
            RecipeId = entity.RecipeId
        };
        return res;
    }
    
}