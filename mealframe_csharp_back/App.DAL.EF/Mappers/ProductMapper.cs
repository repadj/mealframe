using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class ProductMapper : IMapper<App.DAL.DTO.Product, App.Domain.Product>
{
    public Product? Map(Domain.Product? entity)
    {
        if (entity == null) return null;
        var res = new Product()
        {
            Id = entity.Id,
            ProductName = entity.ProductName,
            Calories = entity.Calories,
            Protein = entity.Protein,
            Carbs = entity.Carbs,
            Fat = entity.Fat,
            Sugar = entity.Sugar,
            Salt = entity.Salt,
            CategoryId = entity.CategoryId,
        };
        return res;
    }

    public Domain.Product? Map(Product? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Product()
        {
            Id = entity.Id,
            ProductName = entity.ProductName,
            Calories = entity.Calories,
            Protein = entity.Protein,
            Carbs = entity.Carbs,
            Fat = entity.Fat,
            Sugar = entity.Sugar,
            Salt = entity.Salt,
            CategoryId = entity.CategoryId,
        };
        return res;
    }
}