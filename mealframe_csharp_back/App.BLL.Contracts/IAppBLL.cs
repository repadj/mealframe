using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAppBLL : IBaseBLL
{
    ICategoryService CategoryService { get; }
    IMealEntryService MealEntryService { get; }
    IMealPlanService MealPlanService { get; }
    INutritionalGoalService NutritionalGoalService { get; }
    IProductService ProductService { get; }
    IRecipeService RecipeService { get; }
    IRecipeIngredientService RecipeIngredientService { get; }
    ISavedRecipeService SavedRecipeService { get; }
    IShoppingListService ShoppingListService { get; }
    IShoppingItemService ShoppingItemService { get; }
}