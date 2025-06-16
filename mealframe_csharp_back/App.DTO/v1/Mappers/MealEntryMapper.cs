using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class MealEntryMapper : IMapper<App.DTO.v1.MealEntry, APP.BLL.DTO.MealEntry>
{
    public App.DTO.v1.MealEntry? Map(APP.BLL.DTO.MealEntry? entity)
    {
        if (entity == null) return null;
        var res = new MealEntry()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Unit = entity.Unit,
            MealType = entity.MealType,
            RecipeId = entity.RecipeId,
            ProductId = entity.ProductId,
            MealPlanId = entity.MealPlanId,
        };
        return res;
    }

    public APP.BLL.DTO.MealEntry? Map(App.DTO.v1.MealEntry? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.MealEntry()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Unit = entity.Unit,
            MealType = entity.MealType,
            RecipeId = entity.RecipeId,
            ProductId = entity.ProductId,
            MealPlanId = entity.MealPlanId,
        };
        return res;
    }
    
    public APP.BLL.DTO.MealEntry Map(App.DTO.v1.MealEntryCreate entity)
    {
        var res = new APP.BLL.DTO.MealEntry()
        {
            Id = Guid.NewGuid(),
            Amount = entity.Amount,
            Unit = entity.Unit,
            MealType = entity.MealType,
            RecipeId = entity.RecipeId,
            ProductId = entity.ProductId,
        };
        return res;
    }
    
}