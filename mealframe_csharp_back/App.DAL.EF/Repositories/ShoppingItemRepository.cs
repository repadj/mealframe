using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ShoppingItemRepository : BaseRepository<App.DAL.DTO.ShoppingItem, App.Domain.ShoppingItem>, IShoppingItemRepository
{
    public ShoppingItemRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ShoppingItemMapper())
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.ShoppingItem>> GetItemsByShoppingListIdAsync(Guid shoppingListId, Guid userId)
    {
        var query = RepositoryDbSet
            .Where(x => x.ShoppingListId == shoppingListId && x.ShoppingList!.UserId == userId);

        var items = await query.ToListAsync();

        return items.Select(e => Mapper.Map(e))!;
    }

}