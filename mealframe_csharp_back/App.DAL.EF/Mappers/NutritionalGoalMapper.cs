using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class NutritionalGoalMapper : IMapper<App.DAL.DTO.NutritionalGoal, App.Domain.NutritionalGoal>
{
    public NutritionalGoal? Map(Domain.NutritionalGoal? entity)
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

    public Domain.NutritionalGoal? Map(NutritionalGoal? entity)
    {
        if (entity == null) return null;
        var res = new Domain.NutritionalGoal()
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