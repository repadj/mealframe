using App.BLL.Contracts;
using App.DAL.Contracts;
using App.Domain;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;
using ShoppingList = APP.BLL.DTO.ShoppingList;

namespace App.BLL.Services;

public class ShoppingListService : BaseService<APP.BLL.DTO.ShoppingList, App.DAL.DTO.ShoppingList, App.DAL.Contracts.IShoppingListRepository>, IShoppingListService
{
    
    private readonly IAppUOW _uow;
    private readonly IMapper<ShoppingList, DAL.DTO.ShoppingList> _mapper;
    
    public ShoppingListService(IAppUOW serviceUOW, IMapper<ShoppingList, DAL.DTO.ShoppingList> bllMapper) : base(serviceUOW, serviceUOW.ShoppingListRepository, bllMapper)
    {
        _uow = serviceUOW;
        _mapper = bllMapper;
    }
    
    public async Task<ShoppingList> GenerateFromMealPlansAsync(IEnumerable<Guid> mealPlanIds, Guid userId)
    {
        var shoppingLists = await _uow.ShoppingListRepository.AllAsync(userId);

        foreach (var list in shoppingLists)
        {
            var items = await _uow.ShoppingItemRepository.GetItemsByShoppingListIdAsync(list.Id, userId);
            foreach (var item in items)
            {
                _uow.ShoppingItemRepository.Remove(item.Id, userId);
            }
            _uow.ShoppingListRepository.Remove(list.Id, userId);
        }
        
        await _uow.SaveChangesAsync();
        var shoppingList = new ShoppingList
        {
            Id = Guid.NewGuid(),
            SListName = $"My Shopping List {DateTime.Now:dd-MM-yyyy}",
        };
        
        var dalShoppingList = Mapper.Map(shoppingList)!;

        _uow.ShoppingListRepository.Add(dalShoppingList, userId);
        await _uow.SaveChangesAsync();

        var aggregatedItems = new Dictionary<Guid, decimal>();

        foreach (var mealPlanId in mealPlanIds)
        {
            var entries = await _uow.MealEntryRepository.GetEntriesByMealPlanIdAsync(mealPlanId, userId);
            foreach (var entry in entries)
            {
                if (entry.ProductId != null)
                {
                    aggregatedItems[entry.ProductId.Value] = aggregatedItems.GetValueOrDefault(entry.ProductId.Value) + entry.Amount;
                }
                else if (entry.RecipeId != null)
                {
                    var recipeIngredients = await _uow.RecipeIngredientRepository.GetByRecipeIdAsync(entry.RecipeId.Value);
                    foreach (var ingredient in recipeIngredients)
                    {
                        var scaledAmount = ingredient.Amount * entry.Amount; // entry.Amount = servings
                        aggregatedItems[ingredient.ProductId] = aggregatedItems.GetValueOrDefault(ingredient.ProductId) + scaledAmount;
                    }
                }
            }
        }
        
        foreach (var (productId, totalAmount) in aggregatedItems)
        {
            var shoppingItem = new DAL.DTO.ShoppingItem
            {
                Id = Guid.NewGuid(),
                ShoppingListId = shoppingList.Id,
                ProductId = productId,
                Amount = totalAmount,
                Unit = EUnit.Grams 
            };

            _uow.ShoppingItemRepository.Add(shoppingItem, userId);
        }

        await _uow.SaveChangesAsync();
        return shoppingList;
    }
    
    public async Task<ShoppingList?> GetShoppingListWithItemsAsync(Guid userId)
    {
        var shoppingList = await _uow.ShoppingListRepository.GetDetailedAsync(userId);
        return _mapper.Map(shoppingList);
    }

}