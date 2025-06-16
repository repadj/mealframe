using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class CategoryMapper: IMapper<App.DAL.DTO.Category, App.Domain.Category>
{
    public Category? Map(Domain.Category? entity)
    {
        if (entity == null) return null;
        var res = new Category()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
            Products = entity.Products?.Select(p => new Product()
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                
            }).ToList()
        };
        return res;
    }

    public Domain.Category? Map(Category? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Category()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
            Products = entity.Products?.Select(p => new Domain.Product()
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                
            }).ToList()
        };
        return res;
    }
}