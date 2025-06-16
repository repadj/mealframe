using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Mappers;

public class ShoppingItemMapper : IMapper<App.DAL.DTO.ShoppingItem, App.Domain.ShoppingItem>
{
    public ShoppingItem? Map(Domain.ShoppingItem? entity)
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

    public Domain.ShoppingItem? Map(ShoppingItem? entity)
    {
        if (entity == null) return null;
        var res = new Domain.ShoppingItem()
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