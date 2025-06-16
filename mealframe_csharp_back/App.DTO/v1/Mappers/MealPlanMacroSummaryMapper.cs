using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class MealPlanMacroSummaryMapper : ISimpleMapper<APP.BLL.DTO.MealPlanMacroSummary,MealPlanMacroSummary>
{
    public MealPlanMacroSummary? Map(APP.BLL.DTO.MealPlanMacroSummary? entity)
    {
        if (entity == null) return null;

        return new MealPlanMacroSummary
        {
            Calories = entity.Calories,
            Protein = entity.Protein,
            Carbs = entity.Carbs,
            Fat = entity.Fat,
            Sugar = entity.Sugar,
            Salt = entity.Salt
        };
    }

    public APP.BLL.DTO.MealPlanMacroSummary? Map(MealPlanMacroSummary? entity)
    {
        if (entity == null) return null;

        return new APP.BLL.DTO.MealPlanMacroSummary
        {
            Calories = entity.Calories,
            Protein = entity.Protein,
            Carbs = entity.Carbs,
            Fat = entity.Fat,
            Sugar = entity.Sugar,
            Salt = entity.Salt
        };
    }
}
