using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IShoppingListRepository : IBaseRepository<App.DAL.DTO.ShoppingList>
{
    Task<App.DAL.DTO.ShoppingList?> GetDetailedAsync(Guid userId);
}