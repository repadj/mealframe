using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class ProductMapper : IMapper<APP.BLL.DTO.Product, App.DAL.DTO.Product>
{
    public Product? Map(DAL.DTO.Product? entity)
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

    public DAL.DTO.Product? Map(Product? entity)
    {
        if (entity == null) return null;
        var res = new DAL.DTO.Product()
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