using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DAL.DTO;

public class Category : IDomainId
{
    public Guid Id { get; set; }
    
    public string CategoryName { get; set; } = default!;
    
    public ICollection<Product>? Products { get; set; }
}