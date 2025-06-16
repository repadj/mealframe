using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class MealEntryMapper : IMapper<App.DAL.DTO.MealEntry, App.Domain.MealEntry>
{
    public MealEntry? Map(Domain.MealEntry? entity)
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

    public Domain.MealEntry? Map(MealEntry? entity)
    {
        if (entity == null) return null;
        var res = new Domain.MealEntry()
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
}