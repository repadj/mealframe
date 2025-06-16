using Base.Contracts;

namespace App.DTO.v1.Mappers;

public class ShoppingItemMapper : IMapper<ShoppingItem, APP.BLL.DTO.ShoppingItem>
{
    public ShoppingItem? Map(APP.BLL.DTO.ShoppingItem? entity)
    {
        if (entity == null) return null;
        var res = new ShoppingItem()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Unit = entity.Unit,
            ShoppingListId = entity.ShoppingListId,
            ProductId = entity.ProductId
        };
        return res;
    }

    public APP.BLL.DTO.ShoppingItem? Map(ShoppingItem? entity)
    {
        if (entity == null) return null;
        var res = new APP.BLL.DTO.ShoppingItem()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Unit = entity.Unit,
            ShoppingListId = entity.ShoppingListId,
            ProductId = entity.ProductId
        };
        return res;
    }

    public APP.BLL.DTO.ShoppingItem Map(ShoppingItemCreate entity)
    {
        var res = new APP.BLL.DTO.ShoppingItem()
        {
            Id = Guid.NewGuid(),
            Amount = entity.Amount,
            Unit = entity.Unit,
            ShoppingListId = entity.ShoppingListId,
            ProductId = entity.ProductId
        };
        return res;
    }
    
}