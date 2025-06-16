using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class ShoppingListMapper : IMapper<APP.BLL.DTO.ShoppingList, App.DAL.DTO.ShoppingList>
{
    public ShoppingList? Map(DAL.DTO.ShoppingList? entity)
    {
        if (entity == null) return null;
        var res = new ShoppingList()
        {
            Id = entity.Id,
            SListName = entity.SListName,
            ShoppingItems = entity.ShoppingItems?.Select(s => new ShoppingItem()
            {
                Id = s.Id,
                Amount = s.Amount,
                Unit = s.Unit,
                ProductId = s.ProductId,
                Product = s.Product != null ? new Product
                {
                    Id = s.Product.Id,
                    ProductName = s.Product.ProductName,
                    CategoryId = s.Product.CategoryId,
                    Category = s.Product.Category != null ? new Category
                    {
                        Id = s.Product.Category.Id,
                        CategoryName = s.Product.Category.CategoryName
                    } : null
                } : null
            }).ToList()
        };
        return res;
    }

    public DAL.DTO.ShoppingList? Map(ShoppingList? entity)
    {
        if (entity == null) return null;
        var res = new DAL.DTO.ShoppingList()
        {
            Id = entity.Id,
            SListName = entity.SListName,
            ShoppingItems = entity.ShoppingItems?.Select(s => new DAL.DTO.ShoppingItem()
            {
                Id = s.Id,
                Amount = s.Amount,
                Unit = s.Unit,
                ProductId = s.ProductId
            }).ToList()
        };
        return res;
    }
}