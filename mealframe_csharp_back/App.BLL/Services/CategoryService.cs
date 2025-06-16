using App.BLL.Contracts;
using APP.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;
using Category = App.DAL.DTO.Category;

namespace App.BLL.Services;

public class CategoryService : BaseService<APP.BLL.DTO.Category, App.DAL.DTO.Category, App.DAL.Contracts.ICategoryRepository>, ICategoryService
{
    public CategoryService(IAppUOW serviceUOW, 
        IMapper<APP.BLL.DTO.Category, Category> bllMapper) : base(serviceUOW, serviceUOW.CategoryRepository, bllMapper)
    {
    }
}