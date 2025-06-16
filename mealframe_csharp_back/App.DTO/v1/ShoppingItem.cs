using App.Domain;
using Base.Contracts;

namespace App.DTO.v1;

public class ShoppingItem : IDomainId
{
    public Guid Id { get; set; }
    
    public decimal Amount { get; set; }
    
    public EUnit Unit { get; set; }
    
    public Guid ShoppingListId { get; set; }
    
    public Guid ProductId { get; set; }
}