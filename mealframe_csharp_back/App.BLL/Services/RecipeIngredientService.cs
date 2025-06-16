using App.BLL.Contracts;
using APP.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class RecipeIngredientService : BaseService<APP.BLL.DTO.RecipeIngredient, App.DAL.DTO.RecipeIngredient, App.DAL.Contracts.IRecipeIngredientRepository>, IRecipeIngredientService
{
    public RecipeIngredientService(IAppUOW serviceUOW,
        IMapper<RecipeIngredient, DAL.DTO.RecipeIngredient> bllMapper) : base(serviceUOW, serviceUOW.RecipeIngredientRepository, bllMapper)
    {
    }
}