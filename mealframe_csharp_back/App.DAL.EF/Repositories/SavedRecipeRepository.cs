using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class SavedRecipeRepository : BaseRepository<App.DAL.DTO.SavedRecipe, App.Domain.SavedRecipe>, ISavedRecipeRepository
{
    public SavedRecipeRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new SavedRecipeMapper())
    {
    }

}