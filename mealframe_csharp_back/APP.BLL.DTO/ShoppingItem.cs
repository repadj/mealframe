using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Contracts;

namespace APP.BLL.DTO;

public class ShoppingItem : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Amount), ResourceType = typeof(App.Resources.Domain.ShoppingItem))]
    public decimal Amount { get; set; }
    
    [Display(Name = nameof(Unit), ResourceType = typeof(App.Resources.Domain.ShoppingItem))]
    public EUnit Unit { get; set; }
    
    public Guid ShoppingListId { get; set; }
    
    [Display(Name = nameof(ShoppingList), ResourceType = typeof(App.Resources.Domain.ShoppingItem))]
    public ShoppingList? ShoppingList { get; set; }
    
    public Guid ProductId { get; set; }
    
    [Display(Name = nameof(Product), ResourceType = typeof(App.Resources.Domain.ShoppingItem))]
    public Product? Product { get; set; }
}