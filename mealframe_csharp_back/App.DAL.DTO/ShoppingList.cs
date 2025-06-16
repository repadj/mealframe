using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DAL.DTO;

public class ShoppingList : IDomainId
{
    public Guid Id { get; set; }
    
    public string SListName { get; set; } = default!;
    
    public ICollection<ShoppingItem>? ShoppingItems { get; set; }
}