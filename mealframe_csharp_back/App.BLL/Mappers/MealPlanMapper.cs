using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class MealPlanMapper : IMapper<APP.BLL.DTO.MealPlan, App.DAL.DTO.MealPlan>
{
    public APP.BLL.DTO.MealPlan? Map(App.DAL.DTO.MealPlan? entity)
    {
        if (entity == null) return null;

        return new APP.BLL.DTO.MealPlan
        {
            Id = entity.Id,
            PlanName = entity.PlanName,
            Date = entity.Date,
            MealEntries = entity.MealEntries?.Select(me => new APP.BLL.DTO.MealEntry
            {
                Id = me.Id,
                Amount = me.Amount,
                Unit = me.Unit,
                MealType = me.MealType,
                ProductId = me.ProductId,
                RecipeId = me.RecipeId,
                MealPlanId = me.MealPlanId,

                Product = me.Product != null ? new APP.BLL.DTO.Product
                {
                    Id = me.Product.Id,
                    ProductName = me.Product.ProductName
                } : null,

                Recipe = me.Recipe != null ? new APP.BLL.DTO.Recipe
                {
                    Id = me.Recipe.Id,
                    RecipeName = me.Recipe.RecipeName
                } : null
            }).ToList()
        };
    }

    public App.DAL.DTO.MealPlan? Map(APP.BLL.DTO.MealPlan? entity)
    {
        if (entity == null) return null;

        return new App.DAL.DTO.MealPlan
        {
            Id = entity.Id,
            PlanName = entity.PlanName,
            Date = entity.Date,
            MealEntries = entity.MealEntries?.Select(me => new App.DAL.DTO.MealEntry
            {
                Id = me.Id,
                Amount = me.Amount,
                Unit = me.Unit,
                MealType = me.MealType,
                ProductId = me.ProductId,
                RecipeId = me.RecipeId,
                MealPlanId = me.MealPlanId
            }).ToList()
        };
    }
}