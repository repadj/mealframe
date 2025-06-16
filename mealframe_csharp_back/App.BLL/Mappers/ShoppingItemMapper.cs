using APP.BLL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class ShoppingItemMapper : IMapper<APP.BLL.DTO.ShoppingItem, App.DAL.DTO.ShoppingItem>
{
    public ShoppingItem? Map(DAL.DTO.ShoppingItem? entity)
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

    public DAL.DTO.ShoppingItem? Map(ShoppingItem? entity)
    {
        if (entity == null) return null;
        var res = new DAL.DTO.ShoppingItem()
        {
            Id = entity.Id,
            Amount = entity.Amount,
            Unit = entity.Unit,
            ShoppingListId = entity.ShoppingListId,
            ProductId = entity.ProductId
        };
        return res;
    }
}