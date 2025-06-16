using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class RecipeMapper : IMapper<Recipe, APP.BLL.DTO.Recipe>
{
    public Recipe? Map(APP.BLL.DTO.Recipe? entity)
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
        };
        return res;
    }

    public APP.BLL.DTO.Recipe? Map(Recipe? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.Recipe()
        {
            Id = entity.Id,
            RecipeName = entity.RecipeName,
            Description = entity.Description,
            CookingTime = entity.CookingTime,
            Servings = entity.Servings,
            PictureUrl = entity.PictureUrl,
            Public = entity.Public,
        };
        return res;
    }

    public APP.BLL.DTO.Recipe Map(RecipeCreate entity)
    {
        return new APP.BLL.DTO.Recipe
        {
            Id = Guid.NewGuid(),
            RecipeName = entity.RecipeName,
            Description = entity.Description,
            CookingTime = entity.CookingTime,
            Servings = entity.Servings,
            PictureUrl = entity.PictureUrl,
            Public = entity.Public,
            RecipeIngredients = entity.Ingredients
                .Select(i => new APP.BLL.DTO.RecipeIngredient
                {
                    Id = Guid.NewGuid(),
                    ProductId = i.ProductId,
                    Amount = i.Amount,
                    Unit = i.Unit
                })
                .ToList()
        };
    }
    
    public RecipeDetail MapDetailed(APP.BLL.DTO.Recipe entity)
    {
        return new RecipeDetail
        {
            Id = entity.Id,
            RecipeName = entity.RecipeName,
            Description = entity.Description,
            CookingTime = entity.CookingTime,
            Servings = entity.Servings,
            PictureUrl = entity.PictureUrl,
            Public = entity.Public,
            Ingredients = entity.RecipeIngredients?.Select(ri => new RecipeIngredientDetail
            {
                ProductId = ri.ProductId,
                Amount = ri.Amount,
                Unit = ri.Unit.ToString(), 
                ProductName = ri.Product?.ProductName ?? "Unknown"
            }).ToList() ?? new List<RecipeIngredientDetail>()
        };
    }
    
}