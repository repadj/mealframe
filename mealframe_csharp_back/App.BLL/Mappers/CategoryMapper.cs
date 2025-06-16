using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class CategoryMapper : IMapper<APP.BLL.DTO.Category, App.DAL.DTO.Category>
{
    public Category? Map(DAL.DTO.Category? entity)
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

    public DAL.DTO.Category? Map(Category? entity)
    {
        if (entity == null) return null;
        var res = new DAL.DTO.Category()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
            Products = entity.Products?.Select(p => new DAL.DTO.Product()
            {
                Id = p.Id,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                
            }).ToList()
        };
        return res;
    }
}