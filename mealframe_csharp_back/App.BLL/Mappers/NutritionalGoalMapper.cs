using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class NutritionalGoalMapper : IMapper<APP.BLL.DTO.NutritionalGoal, App.DAL.DTO.NutritionalGoal>
{
    public NutritionalGoal? Map(DAL.DTO.NutritionalGoal? entity)
    {
        if (entity == null) return null;
        var res = new NutritionalGoal()
        {
            Id = entity.Id,
            CalorieTarget = entity.CalorieTarget,
            ProteinTarget = entity.ProteinTarget,
            FatTarget = entity.FatTarget,
            SugarTarget = entity.SugarTarget,
            CarbsTarget = entity.CarbsTarget,
            SaltTarget = entity.SaltTarget
        };
        return res;
    }

    public DAL.DTO.NutritionalGoal? Map(NutritionalGoal? entity)
    {
        if (entity == null) return null;
        var res = new DAL.DTO.NutritionalGoal()
        {
            Id = entity.Id,
            CalorieTarget = entity.CalorieTarget,
            ProteinTarget = entity.ProteinTarget,
            FatTarget = entity.FatTarget,
            SugarTarget = entity.SugarTarget,
            CarbsTarget = entity.CarbsTarget,
            SaltTarget = entity.SaltTarget
        };
        return res;
    }
}