using App.BLL.Contracts;
using APP.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class ShoppingItemService : BaseService<APP.BLL.DTO.ShoppingItem, App.DAL.DTO.ShoppingItem, App.DAL.Contracts.IShoppingItemRepository>, IShoppingItemService
{
    public ShoppingItemService(IAppUOW serviceUOW, IMapper<ShoppingItem, DAL.DTO.ShoppingItem> bllMapper) : base(serviceUOW, serviceUOW.ShoppingItemRepository, bllMapper)
    {
    }
}