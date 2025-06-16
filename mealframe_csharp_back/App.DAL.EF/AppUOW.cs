using App.DAL.Contracts;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext> , IAppUOW
{
    public AppUOW(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }

    private ICategoryRepository? _categoryRepository;
    public ICategoryRepository CategoryRepository => 
        _categoryRepository ??= new CategoryRepository(UOWDbContext);
    
    private IMealEntryRepository? _mealEntryRepository;
    public IMealEntryRepository MealEntryRepository =>
        _mealEntryRepository ??= new MealEntryRepository(UOWDbContext);
    
    private IMealPlanRepository? _mealPlanRepository;
    public IMealPlanRepository MealPlanRepository =>
        _mealPlanRepository ??= new MealPlanRepository(UOWDbContext);
    
    private INutritionalGoalRepository? _nutritionalGoalRepository;
    public INutritionalGoalRepository NutritionalGoalRepository =>
        _nutritionalGoalRepository ??= new NutritionalGoalRepository(UOWDbContext);
    
    private IProductRepository? _productRepository;
    public IProductRepository ProductRepository =>
        _productRepository ??= new ProductRepository(UOWDbContext);
    
    private IRecipeIngredientRepository? _recipeIngredientRepository;
    public IRecipeIngredientRepository RecipeIngredientRepository =>
        _recipeIngredientRepository ??= new RecipeIngredientRepository(UOWDbContext);
    
    private IRecipeRepository? _recipeRepository;
    public IRecipeRepository RecipeRepository =>
        _recipeRepository ??= new RecipeRepository(UOWDbContext);
    
    private ISavedRecipeRepository? _savedRecipeRepository;
    public ISavedRecipeRepository SavedRecipeRepository =>
        _savedRecipeRepository ??= new SavedRecipeRepository(UOWDbContext);
    
    private IShoppingItemRepository? _shoppingItemRepository;
    public IShoppingItemRepository ShoppingItemRepository =>
        _shoppingItemRepository ??= new ShoppingItemRepository(UOWDbContext);
    
    private IShoppingListRepository? _shoppingListRepository;
    public IShoppingListRepository ShoppingListRepository =>
        _shoppingListRepository ??= new ShoppingListRepository(UOWDbContext);
}