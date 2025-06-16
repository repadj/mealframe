using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IShoppingListService : IBaseService<APP.BLL.DTO.ShoppingList>
{
    Task<APP.BLL.DTO.ShoppingList> GenerateFromMealPlansAsync(IEnumerable<Guid> mealPlanIds, Guid userId);

    Task<APP.BLL.DTO.ShoppingList?> GetShoppingListWithItemsAsync(Guid userId);
}