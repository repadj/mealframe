using App.DAL.Contracts;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ProductRepository : BaseRepository<App.DAL.DTO.Product, App.Domain.Product>, IProductRepository
{
    public ProductRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ProductMapper())
    {
    }

}