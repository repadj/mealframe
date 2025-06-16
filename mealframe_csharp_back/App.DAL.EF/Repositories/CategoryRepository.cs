using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CategoryRepository : BaseRepository<App.DAL.DTO.Category, App.Domain.Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new CategoryMapper())
    {
    }
    
    protected override bool UseUserIdFilter => false;
}