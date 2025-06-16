using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class ShoppingListMapper : IMapper<App.DAL.DTO.ShoppingList, App.Domain.ShoppingList>
{
    public ShoppingList? Map(Domain.ShoppingList? entity)
    {
        if (entity == null) return null;

        var res = new ShoppingList
        {
            Id = entity.Id,
            SListName = entity.SListName,
            ShoppingItems = entity.ShoppingItems?.Select(s => new ShoppingItem
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

    public Domain.ShoppingList? Map(ShoppingList? entity)
    {
        if (entity == null) return null;
        var res = new Domain.ShoppingList()
        {
            Id = entity.Id,
            SListName = entity.SListName,
            ShoppingItems = entity.ShoppingItems?.Select(s => new Domain.ShoppingItem()
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