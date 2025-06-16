using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class ShoppingListMapper : IMapper<ShoppingList, APP.BLL.DTO.ShoppingList>
{
    public ShoppingList? Map(APP.BLL.DTO.ShoppingList? entity)
    {
        if (entity == null) return null;
        var res = new ShoppingList()
        {
            Id = entity.Id,
            SListName = entity.SListName,
        };
        return res;
    }

    public APP.BLL.DTO.ShoppingList? Map(ShoppingList? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.ShoppingList()
        {
            Id = entity.Id,
            SListName = entity.SListName,
        };
        return res;
    }

    public APP.BLL.DTO.ShoppingList Map(ShoppingListCreate entity)
    {
        var res = new APP.BLL.DTO.ShoppingList()
        {
            Id = Guid.NewGuid(),
            SListName = entity.SListName,
        };
        return res;
    }
    
    public ShoppingListDetail MapDetailed(APP.BLL.DTO.ShoppingList entity)
    {
        return new ShoppingListDetail()
        {
            Id = entity.Id,
            SListName = entity.SListName,
            ShoppingItems = entity.ShoppingItems?.Select(si => new ShoppingItemDetail
            {
                Id = si.Id,
                Amount = si.Amount,
                Unit = si.Unit,
                ProductId = si.ProductId,
                ProductName = si.Product?.ProductName,
                ProductCategory = si.Product?.Category?.CategoryName
            }).ToList()
        };
    }
    
}