using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class RecipeIngredientMapper : IMapper<RecipeIngredient, APP.BLL.DTO.RecipeIngredient>
{
    public RecipeIngredient? Map(APP.BLL.DTO.RecipeIngredient? entity)
    {
        if (entity == null) return null;
        var res = new RecipeIngredient()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Unit = entity.Unit,
            RecipeId = entity.RecipeId,
            ProductId = entity.ProductId
        };
        return res;
    }

    public APP.BLL.DTO.RecipeIngredient? Map(RecipeIngredient? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.RecipeIngredient()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Unit = entity.Unit,
            RecipeId = entity.RecipeId,
            ProductId = entity.ProductId
        };
        return res;
    }

    public APP.BLL.DTO.RecipeIngredient Map(RecipeIngredientCreate entity)
    {
        var res = new APP.BLL.DTO.RecipeIngredient()
        {
            Id = Guid.NewGuid(),
            Amount = entity.Amount,
            Unit = entity.Unit,
            RecipeId = Guid.NewGuid(),
            ProductId = entity.ProductId
        };
        return res;
    }
    
}