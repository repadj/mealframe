using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MealEntryRepository : BaseRepository<App.DAL.DTO.MealEntry, App.Domain.MealEntry>, IMealEntryRepository
{
    public MealEntryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new MealEntryMapper())
    {
    }

    public async Task<IEnumerable<App.DAL.DTO.MealEntry>> GetEntriesByMealPlanIdAsync(Guid mealPlanId, Guid userId)
    {
        var entries = await RepositoryDbSet
            .Where(e => e.MealPlanId == mealPlanId && e.MealPlan!.UserId == userId)
            .ToListAsync();

        return entries.Select(e => Mapper.Map(e))!;
    }

    public async Task RemoveByMealPlanIdAsync(Guid mealPlanId, Guid userId)
    {
        var entries = await RepositoryDbSet
            .Where(e => e.MealPlanId == mealPlanId && e.MealPlan!.UserId == userId)
            .ToListAsync();

        RepositoryDbSet.RemoveRange(entries);
    }
}