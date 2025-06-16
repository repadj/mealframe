using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class MealPlanRepository : BaseRepository<App.DAL.DTO.MealPlan, App.Domain.MealPlan>, IMealPlanRepository
{
    public MealPlanRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new MealPlanMapper())
    {
    }
    
    public async Task<App.Domain.MealPlan?> FindDomainAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(mp => mp.MealEntries)
            .FirstOrDefaultAsync(mp => mp.Id == id && mp.UserId == userId);
    }

    public async Task<App.DAL.DTO.MealPlan?> FirstOrDefaultDetailedAsync(Guid id, Guid userId)
    {
        var domainEntity = await RepositoryDbSet
            .Include(mp => mp.MealEntries)!
            .ThenInclude(me => me.Product)
            .Include(mp => mp.MealEntries)!
            .ThenInclude(me => me.Recipe)
            .FirstOrDefaultAsync(mp => mp.Id == id && mp.UserId == userId);

        return domainEntity == null ? null : Mapper.Map(domainEntity);
    }
    
    public async Task<IEnumerable<App.DAL.DTO.MealPlan>> GetAllDetailedAsync(Guid userId)
    {
        var query = RepositoryDbSet
            .Where(mp => mp.UserId == userId)
            .Include(mp => mp.MealEntries)!
            .ThenInclude(me => me.Product)
            .Include(mp => mp.MealEntries)!
            .ThenInclude(me => me.Recipe);

        return (await query.ToListAsync()).Select(e => Mapper.Map(e))!;
    }
}