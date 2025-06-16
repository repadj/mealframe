using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class NutritionalGoalMapper : IMapper<NutritionalGoal, APP.BLL.DTO.NutritionalGoal>
{
    public NutritionalGoal? Map(APP.BLL.DTO.NutritionalGoal? entity)
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

    public APP.BLL.DTO.NutritionalGoal? Map(NutritionalGoal? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.NutritionalGoal()
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

    public APP.BLL.DTO.NutritionalGoal Map(NutritionalGoalCreate entity)
    {
        var res = new APP.BLL.DTO.NutritionalGoal()
        {
            Id = Guid.NewGuid(),
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