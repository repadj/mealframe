using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class CategoryMapper : IMapper<App.DTO.v1.Category, APP.BLL.DTO.Category>
{
    public App.DTO.v1.Category? Map(APP.BLL.DTO.Category? entity)
    {
        if (entity == null) return null;
        var res = new Category()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
        };
        return res;
    }

    public APP.BLL.DTO.Category? Map(Category? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.Category()
        {
            Id = entity.Id,
            CategoryName = entity.CategoryName,
        };
        return res;
    }
    
    public APP.BLL.DTO.Category Map(CategoryCreate entity)
    {
        var res = new APP.BLL.DTO.Category()
        {
            Id = Guid.NewGuid(),
            CategoryName = entity.CategoryName,
        };
        return res;
    }
    
}