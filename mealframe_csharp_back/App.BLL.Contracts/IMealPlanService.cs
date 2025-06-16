using APP.BLL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IMealPlanService : IBaseService<APP.BLL.DTO.MealPlan>
{
    Task<APP.BLL.DTO.MealPlan?> GetMealPlanWithEntriesAsync(Guid id, Guid userId);
    Task<IEnumerable<APP.BLL.DTO.MealPlan>> GetAllWithEntriesAsync(Guid userId);
    Task<APP.BLL.DTO.MealPlan> UpdateWithEntriesAsync(APP.BLL.DTO.MealPlan mealPlan, Guid userId);
    Task<APP.BLL.DTO.MealPlan> CreateWithEntriesAsync(APP.BLL.DTO.MealPlan mealPlan, Guid userId);
    Task RemoveWithEntriesAsync(Guid id, Guid userId);
    Task<MealPlanMacroSummary?> GetMealPlanMacrosAsync(Guid mealPlanId, Guid userId);
}