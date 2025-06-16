using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class ShoppingItem : IDomainId
{
    public Guid Id { get; set; }
    
    public decimal Amount { get; set; }
    
    public EUnit Unit { get; set; }
    
    public Guid ShoppingListId { get; set; }
    public ShoppingList? ShoppingList { get; set; }
    
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}