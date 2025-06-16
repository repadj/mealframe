using App.BLL.Contracts;
using APP.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class SavedRecipeService : BaseService<APP.BLL.DTO.SavedRecipe, App.DAL.DTO.SavedRecipe, App.DAL.Contracts.ISavedRecipeRepository>, ISavedRecipeService
{
    public SavedRecipeService(IAppUOW serviceUOW, 
        IMapper<SavedRecipe, DAL.DTO.SavedRecipe> bllMapper) : base(serviceUOW, serviceUOW.SavedRecipeRepository, bllMapper)
    {
    }
}