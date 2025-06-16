using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IRecipeRepository : IBaseRepository<App.DAL.DTO.Recipe>
{
    Task<App.DAL.DTO.Recipe?> FirstOrDefaultDetailedAsync(Guid id, Guid userId);
}