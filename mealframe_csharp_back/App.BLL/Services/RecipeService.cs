using System.Runtime.CompilerServices;
using App.BLL.Contracts;
using APP.BLL.DTO;
using App.DAL.Contracts;
using App.Domain;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;
using Microsoft.Extensions.Logging;
using Recipe = APP.BLL.DTO.Recipe;
using RecipeIngredient = APP.BLL.DTO.RecipeIngredient;

namespace App.BLL.Services;

public class RecipeService : BaseService<APP.BLL.DTO.Recipe, App.DAL.DTO.Recipe, App.DAL.Contracts.IRecipeRepository>, IRecipeService
{
    protected readonly IAppUOW _serviceUOW;
    
    public RecipeService(IAppUOW serviceUOW, IMapper<Recipe, DAL.DTO.Recipe> bllMapper) : base(serviceUOW, serviceUOW.RecipeRepository, bllMapper)
    {
        _serviceUOW = serviceUOW;
    }
    
    public async Task<Recipe?> GetDetailedAsync(Guid id, Guid userId)
    {
        var recipe = await _serviceUOW.RecipeRepository
            .FirstOrDefaultDetailedAsync(id, userId);

        return Mapper.Map(recipe);
    }
    
    public async Task<Recipe> CreateWithIngredientsAsync(Recipe recipe, Guid userId)
    {
        var mappedRecipe = Mapper.Map(recipe)!;
        mappedRecipe.RecipeIngredients = null; 
        _serviceUOW.RecipeRepository.Add(mappedRecipe, userId);
        
        await _serviceUOW.SaveChangesAsync();
        
        foreach (var ingredient in recipe.RecipeIngredients ?? new List<RecipeIngredient>())
        {
            var dalIngredient = new DAL.DTO.RecipeIngredient
            {
                Id = Guid.NewGuid(),
                RecipeId = mappedRecipe.Id,
                ProductId = ingredient.ProductId,
                Amount = ingredient.Amount,
                Unit = ingredient.Unit
            };

            _serviceUOW.RecipeIngredientRepository.Add(dalIngredient);
        }
        
        await _serviceUOW.SaveChangesAsync();

        recipe.Id = mappedRecipe.Id;
        return recipe;
    }
    
    public async Task<Recipe> UpdateWithIngredientsAsync(Recipe recipe, Guid userId)
    {
        await _serviceUOW.RecipeIngredientRepository.RemoveByRecipeIdAsync(recipe.Id);
        
        var mappedRecipe = Mapper.Map(recipe)!;
        mappedRecipe.RecipeIngredients = null;

        _serviceUOW.RecipeRepository.Update(mappedRecipe);

        await _serviceUOW.SaveChangesAsync();
        
        foreach (var ingredient in recipe.RecipeIngredients!)
        {
            var dalIngredient = new DAL.DTO.RecipeIngredient
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe.Id,
                ProductId = ingredient.ProductId,
                Amount = ingredient.Amount,
                Unit = ingredient.Unit
            };

            _serviceUOW.RecipeIngredientRepository.Add(dalIngredient);
        }

        await _serviceUOW.SaveChangesAsync();

        return recipe;
    }

    public async Task RemoveRecipeWithIngredientsAsync(Guid id, Guid userId = default)
    {
        await _serviceUOW.RecipeIngredientRepository.RemoveByRecipeIdAsync(id);
        await base.RemoveAsync(id, userId);
        await _serviceUOW.SaveChangesAsync();
    }
    
    public async Task<RecipeMacroSummary?> GetRecipeMacrosPerServingAsync(Guid recipeId, Guid userId)
    {
        var recipe = await _serviceUOW.RecipeRepository.FirstOrDefaultDetailedAsync(recipeId, userId);
        if (recipe == null || recipe.Servings <= 0) return null;

        var macroSummary = new RecipeMacroSummary();

        foreach (var ingredient in recipe.RecipeIngredients ?? new List<DAL.DTO.RecipeIngredient>())
        {
            var product = ingredient.Product;
            if (product == null) continue;

            decimal factor = ingredient.Amount / 100m;

            macroSummary.Calories += product.Calories * factor;
            macroSummary.Protein += product.Protein * factor;
            macroSummary.Carbs += product.Carbs * factor;
            macroSummary.Fat += product.Fat * factor;
            macroSummary.Salt += product.Salt * factor;
            macroSummary.Sugar += product.Sugar * factor;
        }
        
        macroSummary.Calories /= recipe.Servings;
        macroSummary.Protein /= recipe.Servings;
        macroSummary.Carbs /= recipe.Servings;
        macroSummary.Fat /= recipe.Servings;
        macroSummary.Salt /= recipe.Servings;
        macroSummary.Sugar /= recipe.Servings;

        return macroSummary;
    }

}