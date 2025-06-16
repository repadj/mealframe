using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IMealEntryRepository : IBaseRepository<App.DAL.DTO.MealEntry>
{
    Task<IEnumerable<App.DAL.DTO.MealEntry>> GetEntriesByMealPlanIdAsync(Guid mealPlanId, Guid userId);

    Task RemoveByMealPlanIdAsync(Guid mealPlanId, Guid userId);
}