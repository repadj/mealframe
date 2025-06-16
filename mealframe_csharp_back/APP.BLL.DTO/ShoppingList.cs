using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace APP.BLL.DTO;

public class ShoppingList : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(32)]
    [Display(Name = nameof(SListName), ResourceType = typeof(App.Resources.Domain.ShoppingList))]
    public string SListName { get; set; } = default!;
    public ICollection<ShoppingItem>? ShoppingItems { get; set; }
}