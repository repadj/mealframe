using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IMealPlanRepository : IBaseRepository<App.DAL.DTO.MealPlan>
{
    Task<App.Domain.MealPlan?> FindDomainAsync(Guid id, Guid userId);
    Task<App.DAL.DTO.MealPlan?> FirstOrDefaultDetailedAsync(Guid id, Guid userId);

    Task<IEnumerable<App.DAL.DTO.MealPlan>> GetAllDetailedAsync(Guid userId);
}