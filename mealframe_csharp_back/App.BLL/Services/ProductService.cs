using App.BLL.Contracts;
using APP.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class ProductService : BaseService<APP.BLL.DTO.Product, App.DAL.DTO.Product, App.DAL.Contracts.IProductRepository>, IProductService
{
    public ProductService(IAppUOW serviceUOW, 
        IMapper<Product, DAL.DTO.Product> bllMapper) : base(serviceUOW, serviceUOW.ProductRepository, bllMapper)
    {
    }
}