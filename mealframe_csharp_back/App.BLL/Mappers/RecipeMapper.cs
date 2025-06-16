using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class RecipeMapper : IMapper<APP.BLL.DTO.Recipe, App.DAL.DTO.Recipe>
{
    public Recipe? Map(DAL.DTO.Recipe? entity)
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
            RecipeIngredients = entity.RecipeIngredients?.Select(x => new RecipeIngredient()
            {
                Id = x.Id,
                Amount = x.Amount,
                Unit = x.Unit,
                ProductId = x.ProductId,
                Product = x.Product == null ? null : new Product
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

    public DAL.DTO.Recipe? Map(Recipe? entity)
    {
        if (entity == null) return null;
        var res = new DAL.DTO.Recipe()
        {
            Id = entity.Id,
            RecipeName = entity.RecipeName,
            Description = entity.Description,
            CookingTime = entity.CookingTime,
            Servings = entity.Servings,
            PictureUrl = entity.PictureUrl,
            Public = entity.Public,
            RecipeIngredients = entity.RecipeIngredients?.Select(x => new DAL.DTO.RecipeIngredient()
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