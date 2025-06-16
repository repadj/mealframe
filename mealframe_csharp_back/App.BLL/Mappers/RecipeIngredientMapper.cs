using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class RecipeIngredientMapper : IMapper<APP.BLL.DTO.RecipeIngredient, App.DAL.DTO.RecipeIngredient>
{
    public RecipeIngredient? Map(DAL.DTO.RecipeIngredient? entity)
    {
        if (entity == null) return null;
        var res = new RecipeIngredient()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Unit = entity.Unit,
            RecipeId = entity.RecipeId,
            ProductId = entity.ProductId,
            Product = entity.Product == null ? null : new Product
            {
                Id = entity.Product.Id,
                ProductName = entity.Product.ProductName,
                Calories = entity.Product.Calories,
                Protein = entity.Product.Protein,
                Carbs = entity.Product.Carbs,
                Fat = entity.Product.Fat,
                Sugar = entity.Product.Sugar,
                Salt = entity.Product.Salt,
                CategoryId = entity.Product.CategoryId
            }
        };
        return res;
    }

    public DAL.DTO.RecipeIngredient? Map(RecipeIngredient? entity)
    {
        if (entity == null) return null;
        var res = new DAL.DTO.RecipeIngredient()
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