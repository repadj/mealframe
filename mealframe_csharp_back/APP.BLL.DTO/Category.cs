using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace APP.BLL.DTO;

public class Category : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)] 
    [Display(Name = nameof(CategoryName), ResourceType = typeof(App.Resources.Domain.Category))]
    public string CategoryName { get; set; } = default!;
    
    public ICollection<Product>? Products { get; set; }
}