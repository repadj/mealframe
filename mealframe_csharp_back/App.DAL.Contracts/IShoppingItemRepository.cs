using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IShoppingItemRepository : IBaseRepository<App.DAL.DTO.ShoppingItem>
{
    Task<IEnumerable<App.DAL.DTO.ShoppingItem>> GetItemsByShoppingListIdAsync(Guid shoppingListId, Guid userId);
}