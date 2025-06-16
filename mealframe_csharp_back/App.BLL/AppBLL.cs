using App.BLL.Contracts;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.Contracts;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    public AppBLL(IAppUOW uow) : base(uow)
    {
    }

    private ICategoryService? _categoryService;
    public ICategoryService CategoryService => 
        _categoryService ??= new CategoryService(BllUow, new CategoryMapper() );
    
    private IMealEntryService? _mealEntryService;
    public IMealEntryService MealEntryService => 
        _mealEntryService ??= new MealEntryService(BllUow, new MealEntryMapper() );

    private IMealPlanService? _mealPlanService;
    public IMealPlanService MealPlanService => 
        _mealPlanService ??= new MealPlanService(BllUow, RecipeService, new MealPlanMapper() );
    
    private INutritionalGoalService? _nutritionalGoalService;
    public INutritionalGoalService NutritionalGoalService => 
        _nutritionalGoalService ??= new NutritionalGoalService(BllUow, new NutritionalGoalMapper() );
    
    private IProductService? _productService;
    public IProductService ProductService => 
        _productService ??= new ProductService(BllUow, new ProductMapper() );
    
    private IRecipeService? _recipeService;
    public IRecipeService RecipeService => 
        _recipeService ??= new RecipeService(BllUow, new RecipeMapper() );
    
    private IRecipeIngredientService? _recipeIngredientService;
    public IRecipeIngredientService RecipeIngredientService => 
        _recipeIngredientService ??= new RecipeIngredientService(BllUow, new RecipeIngredientMapper() );
    
    private ISavedRecipeService? _savedRecipeService;
    public ISavedRecipeService SavedRecipeService => 
        _savedRecipeService ??= new SavedRecipeService(BllUow, new SavedRecipeMapper() );
    
    private IShoppingListService? _shoppingListService;
    public IShoppingListService ShoppingListService => 
        _shoppingListService ??= new ShoppingListService(BllUow, new ShoppingListMapper() );
    
    private IShoppingItemService? _shoppingItemService;
    public IShoppingItemService ShoppingItemService => 
        _shoppingItemService ??= new ShoppingItemService(BllUow, new ShoppingItemMapper() );
}