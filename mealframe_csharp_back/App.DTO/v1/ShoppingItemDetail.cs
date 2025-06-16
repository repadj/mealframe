using App.Domain;
using Base.Contracts;

namespace App.DTO.v1;

public class ShoppingItemDetail : IDomainId
{
    public Guid Id { get; set; }
    
    public decimal Amount { get; set; }
    
    
    public EUnit Unit { get; set; }
    
    public Guid ProductId { get; set; }
    
    public string? ProductName { get; set; }
    
    public string? ProductCategory { get; set; }
}