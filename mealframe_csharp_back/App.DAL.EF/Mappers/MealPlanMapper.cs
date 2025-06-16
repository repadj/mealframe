using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class MealPlanMapper : IMapper<App.DAL.DTO.MealPlan, App.Domain.MealPlan>
{
    public MealPlan? Map(Domain.MealPlan? entity)
    {
        if (entity == null) return null;

        return new MealPlan
        {
            Id = entity.Id,
            PlanName = entity.PlanName,
            Date = entity.Date,
            MealEntries = entity.MealEntries?.Select(me => new MealEntry
            {
                Id = me.Id,
                Amount = me.Amount,
                Unit = me.Unit,
                MealType = me.MealType,
                RecipeId = me.RecipeId,
                ProductId = me.ProductId,
                MealPlanId = me.MealPlanId,
                
                Product = me.Product != null ? new Product
                {
                    Id = me.Product.Id,
                    ProductName = me.Product.ProductName,
                    Calories = me.Product.Calories,
                    Protein = me.Product.Protein,
                    Carbs = me.Product.Carbs,
                    Fat = me.Product.Fat,
                    Sugar = me.Product.Sugar,
                    Salt = me.Product.Salt
                } : null,

                Recipe = me.Recipe != null ? new Recipe
                {
                    Id = me.Recipe.Id,
                    RecipeName = me.Recipe.RecipeName
                } : null
            }).ToList()
        };
    }

    public Domain.MealPlan? Map(MealPlan? entity)
    {
        if (entity == null) return null;

        return new Domain.MealPlan
        {
            Id = entity.Id,
            PlanName = entity.PlanName,
            Date = entity.Date,
            MealEntries = entity.MealEntries?.Select(me => new Domain.MealEntry
            {
                Id = me.Id,
                Amount = me.Amount,
                Unit = me.Unit,
                MealType = me.MealType,
                RecipeId = me.RecipeId,
                ProductId = me.ProductId,
                MealPlanId = me.MealPlanId,
                
                Product = me.Product != null ? new Domain.Product
                {
                    Id = me.Product.Id,
                    ProductName = me.Product.ProductName
                } : null,
                
                Recipe = me.Recipe != null ? new Domain.Recipe
                {
                    Id = me.Recipe.Id,
                    RecipeName = me.Recipe.RecipeName
                } : null
            }).ToList()
        };
    }
}