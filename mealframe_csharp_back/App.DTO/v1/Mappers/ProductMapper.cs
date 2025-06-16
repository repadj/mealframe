using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class ProductMapper : IMapper<Product, APP.BLL.DTO.Product>
{
    public Product? Map(APP.BLL.DTO.Product? entity)
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

    public APP.BLL.DTO.Product? Map(Product? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.Product()
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

    public APP.BLL.DTO.Product Map(ProductCreate entity)
    {
        var res = new APP.BLL.DTO.Product()
        {
            Id = Guid.NewGuid(),
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