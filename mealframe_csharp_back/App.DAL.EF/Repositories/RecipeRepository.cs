using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RecipeRepository : BaseRepository<App.DAL.DTO.Recipe, App.Domain.Recipe>, IRecipeRepository
{
    public RecipeRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new RecipeMapper())
    {
    }
    
    public async Task<App.DAL.DTO.Recipe?> FirstOrDefaultDetailedAsync(Guid id, Guid userId)
    {
        var domainEntity = await RepositoryDbSet
            .Include(r => r.RecipeIngredients!)
            .ThenInclude(ri => ri.Product!)
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
        
        return domainEntity == null ? null : Mapper.Map(domainEntity);
    }

}