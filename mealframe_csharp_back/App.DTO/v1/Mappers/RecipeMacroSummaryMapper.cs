using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class RecipeMacroSummaryMapper : ISimpleMapper<APP.BLL.DTO.RecipeMacroSummary, RecipeMacroSummary>
{
    public RecipeMacroSummary? Map(APP.BLL.DTO.RecipeMacroSummary? entity)
    {
        if (entity == null) return null;
        return new RecipeMacroSummary
        {
            Calories = entity.Calories,
            Protein = entity.Protein,
            Carbs = entity.Carbs,
            Fat = entity.Fat,
            Sugar = entity.Sugar,
            Salt = entity.Salt
        };
    }

    public APP.BLL.DTO.RecipeMacroSummary? Map(RecipeMacroSummary? entity)
    {
        if (entity == null) return null;
        return new APP.BLL.DTO.RecipeMacroSummary
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