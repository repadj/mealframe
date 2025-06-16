Useful commands and scripts for working with the project.

docker-compose up --build

.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres");

MIGRATIONS
dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext InitialCreate

dotnet ef migrations   --project App.DAL.EF --startup-project WebApp remove
dotnet ef database   --project App.DAL.EF --startup-project WebApp update
dotnet ef database   --project App.DAL.EF --startup-project WebApp drop

MVC CONTROLLER
cd WebApp

dotnet aspnet-codegenerator controller -name UsersController        -actions -m  Domain.User        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShoppingListsController        -actions -m  App.Domain.ShoppingList        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ShoppingItemsController        -actions -m  App.Domain.ShoppingItem        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name SavedRecipesController        -actions -m  App.Domain.SavedRecipe        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RecipeIngredientsController        -actions -m  App.Domain.RecipeIngredient        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RecipesController        -actions -m  App.Domain.Recipe       -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ProductsController        -actions -m  App.Domain.Product        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PlansOfUsersController        -actions -m  App.Domain.PlansOfUser        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name NutritionalGoalsController        -actions -m  App.Domain.NutritionalGoal       -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MealPlansController        -actions -m  App.Domain.MealPlan        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MealEntriesController        -actions -m  App.Domain.MealEntry        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoriesController        -actions -m  App.Domain.Category       -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

API CONTROLLER
dotnet aspnet-codegenerator controller -name CategoriesController  -m  App.Domain.Category        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name MealEntriesController  -m  App.Domain.MealEntry        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name MealPlansController  -m  App.Domain.MealPlan        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name NutritionalGoalsController  -m  App.Domain.NutritionalGoal        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name PlansOfUsersController  -m  App.Domain.PlansOfUser        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ProductsController  -m  App.Domain.Product        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name RecipesController  -m  App.Domain.Recipe       -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name RecipeIngredientsController  -m  App.Domain.RecipeIngredient        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name SavedRecipesController  -m  App.Domain.SavedRecipe        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ShoppingItemsController  -m  App.Domain.ShoppingItem       -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ShoppingListsController  -m  App.Domain.ShoppingList        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f



