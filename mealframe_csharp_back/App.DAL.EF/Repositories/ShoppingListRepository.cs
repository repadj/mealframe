using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ShoppingListRepository : BaseRepository<App.DAL.DTO.ShoppingList, App.Domain.ShoppingList>, IShoppingListRepository
{
    public ShoppingListRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ShoppingListMapper())
    {
    }
    
    public async Task<App.DAL.DTO.ShoppingList?> GetDetailedAsync(Guid userId)
    {
        var domainEntity = await RepositoryDbSet
            .Include(s => s.ShoppingItems)!
            .ThenInclude(si => si.Product)
            .ThenInclude(p => p!.Category)
            .FirstOrDefaultAsync(s => s.UserId == userId);

        return Mapper.Map(domainEntity);
    }
    
}