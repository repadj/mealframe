using App.BLL.Contracts;
using APP.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class MealEntryService : BaseService<APP.BLL.DTO.MealEntry, App.DAL.DTO.MealEntry, App.DAL.Contracts.IMealEntryRepository>, IMealEntryService
{
    public MealEntryService(IAppUOW serviceUOW, 
        IMapper<MealEntry, DAL.DTO.MealEntry> bllMapper) : base(serviceUOW, serviceUOW.MealEntryRepository, bllMapper)
    {
    }
}