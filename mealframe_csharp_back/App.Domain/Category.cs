using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Category : BaseEntity
{
    [MaxLength(128)] 
    [Display(Name = nameof(CategoryName), ResourceType = typeof(App.Resources.Domain.Category))]
    public string CategoryName { get; set; } = default!;
    
    public ICollection<Product>? Products { get; set; }
}