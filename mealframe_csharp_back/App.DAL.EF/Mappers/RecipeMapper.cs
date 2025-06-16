using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class RecipeMapper : IMapper<App.DAL.DTO.Recipe, App.Domain.Recipe>
{
    public Recipe? Map(Domain.Recipe? entity)
    {
        if (entity == null) return null;
        var res = new Recipe()
        {
            Id = entity.Id,
            RecipeName = entity.RecipeName,
            Description = entity.Description,
            CookingTime = entity.CookingTime,
            Servings = entity.Servings,
            PictureUrl = entity.PictureUrl,
            Public = entity.Public,
            RecipeIngredients = entity.RecipeIngredients?.Select(x => new DAL.DTO.RecipeIngredient
            {
                Id = x.Id,
                Amount = x.Amount,
                Unit = x.Unit,
                RecipeId = x.RecipeId,
                ProductId = x.ProductId,
                Product = x.Product == null ? null : new DAL.DTO.Product
                {
                    Id = x.Product.Id,
                    ProductName = x.Product.ProductName,
                    Calories = x.Product.Calories,
                    Protein = x.Product.Protein,
                    Carbs = x.Product.Carbs,
                    Fat = x.Product.Fat,
                    Sugar = x.Product.Sugar,
                    Salt = x.Product.Salt,
                    CategoryId = x.Product.CategoryId
                }
            }).ToList()
        };
        return res;
    }

    public Domain.Recipe? Map(Recipe? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Recipe()
        {
            Id = entity.Id,
            RecipeName = entity.RecipeName,
            Description = entity.Description,
            CookingTime = entity.CookingTime,
            Servings = entity.Servings,
            PictureUrl = entity.PictureUrl,
            Public = entity.Public,
            RecipeIngredients = entity.RecipeIngredients?.Select(x => new Domain.RecipeIngredient()
            {
                Id = x.Id,
                Amount = x.Amount,
                Unit = x.Unit,
                ProductId = x.ProductId
            }).ToList()
        };
        return res;
    }
}