using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUOW : IBaseUOW
{
    ICategoryRepository CategoryRepository { get; }
    IMealEntryRepository MealEntryRepository { get; }
    IMealPlanRepository MealPlanRepository { get; }
    INutritionalGoalRepository NutritionalGoalRepository { get; }
    IProductRepository ProductRepository { get; }
    IRecipeIngredientRepository RecipeIngredientRepository { get; }
    IRecipeRepository RecipeRepository { get; }
    ISavedRecipeRepository SavedRecipeRepository { get; }
    IShoppingItemRepository ShoppingItemRepository { get; }
    IShoppingListRepository ShoppingListRepository { get; }
}