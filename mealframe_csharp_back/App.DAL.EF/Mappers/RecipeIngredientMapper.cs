using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class RecipeIngredientMapper : IMapper<App.DAL.DTO.RecipeIngredient, App.Domain.RecipeIngredient>
{
    public RecipeIngredient? Map(Domain.RecipeIngredient? entity)
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

    public Domain.RecipeIngredient? Map(RecipeIngredient? entity)
    {
        if (entity == null) return null;
        var res = new Domain.RecipeIngredient()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Unit = entity.Unit,
            RecipeId = entity.RecipeId,
            ProductId = entity.ProductId
        };
        return res;
    }
}