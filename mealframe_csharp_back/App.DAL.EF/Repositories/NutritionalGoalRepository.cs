using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class NutritionalGoalRepository : BaseRepository<App.DAL.DTO.NutritionalGoal, App.Domain.NutritionalGoal>, INutritionalGoalRepository
{
    public NutritionalGoalRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new NutritionalGoalMapper())
    {
    }

}