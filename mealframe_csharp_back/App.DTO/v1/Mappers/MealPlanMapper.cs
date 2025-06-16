using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class MealPlanMapper : IMapper<App.DTO.v1.MealPlan, APP.BLL.DTO.MealPlan>
{
    public App.DTO.v1.MealPlan? Map(APP.BLL.DTO.MealPlan? entity)
    {
        if (entity == null) return null;
        var res = new MealPlan()
        {
            Id = entity.Id,
            PlanName = entity.PlanName,
            Date = entity.Date,
        };
        return res;
    }

    public APP.BLL.DTO.MealPlan? Map(App.DTO.v1.MealPlan? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.MealPlan()
        {
            Id = entity.Id,
            PlanName = entity.PlanName,
            Date = entity.Date,
        };
        return res;
    }
    
    public APP.BLL.DTO.MealPlan Map(App.DTO.v1.MealPlanCreate entity)
    {
        var res = new APP.BLL.DTO.MealPlan
        {
            Id = Guid.NewGuid(),
            PlanName = entity.PlanName,
            Date = entity.Date,
            MealEntries = entity.MealEntries?.Select(e => new APP.BLL.DTO.MealEntry
            {
                Id = Guid.NewGuid(),
                Amount = e.Amount,
                Unit = e.Unit,
                MealType = e.MealType,
                ProductId = e.ProductId,
                RecipeId = e.RecipeId,
                MealPlanId = Guid.Empty
            }).ToList()
        };
        return res;
    }
    
    public App.DTO.v1.MealPlanDetail? MapDetailed(APP.BLL.DTO.MealPlan? entity)
    {
        if (entity == null) return null;
        var res = new MealPlanDetail()
        {
            Id = entity.Id,
            PlanName = entity.PlanName,
            Date = entity.Date,
            MealEntries = entity.MealEntries?.Select(me => new MealEntryDetail()
            {
                Id = me.Id,
                Amount = me.Amount,
                Unit = me.Unit,
                MealType = me.MealType,
                RecipeId = me.RecipeId,
                RecipeName = me.Recipe?.RecipeName,
                ProductId = me.ProductId,
                ProductName = me.Product?.ProductName,
                MealPlanId = me.MealPlanId
            }).ToList() ?? new List<MealEntryDetail>(),
        };

        return res;
    }
    
}