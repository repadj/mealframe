using App.BLL.Contracts;
using APP.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class NutritionalGoalService : BaseService<APP.BLL.DTO.NutritionalGoal, App.DAL.DTO.NutritionalGoal, App.DAL.Contracts.INutritionalGoalRepository>, INutritionalGoalService
{
    public NutritionalGoalService(IAppUOW serviceUOW, 
        IMapper<NutritionalGoal, DAL.DTO.NutritionalGoal> bllMapper) : base(serviceUOW, serviceUOW.NutritionalGoalRepository, bllMapper)
    {
    }
}