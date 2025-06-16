using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RecipeIngredientRepository : BaseRepository<App.DAL.DTO.RecipeIngredient, App.Domain.RecipeIngredient>, IRecipeIngredientRepository
{
    public RecipeIngredientRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new RecipeIngredientMapper())
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.RecipeIngredient>> GetByRecipeIdAsync(Guid recipeId)
    {
        var ingredients = await RepositoryDbSet
            .Where(x => x.RecipeId == recipeId)
            .ToListAsync();

        return ingredients.Select(x => Mapper.Map(x))!;
    }

    public async Task RemoveByRecipeIdAsync(Guid recipeId)
    {
        var ingredients = await RepositoryDbSet
            .Where(x => x.RecipeId == recipeId)
            .ToListAsync();

        RepositoryDbSet.RemoveRange(ingredients);
    }
}