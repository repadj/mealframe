using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class MealEntryMapper : IMapper<APP.BLL.DTO.MealEntry, App.DAL.DTO.MealEntry>
{
    public MealEntry? Map(DAL.DTO.MealEntry? entity)
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

    public DAL.DTO.MealEntry? Map(MealEntry? entity)
    {
        if (entity == null) return null;
        var res = new DAL.DTO.MealEntry()
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