using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class ShoppingList : BaseEntityUser<AppUser>
{
    [MaxLength(32)]
    [Display(Name = nameof(SListName), ResourceType = typeof(App.Resources.Domain.ShoppingList))]
    public string SListName { get; set; } = default!;
    public ICollection<ShoppingItem>? ShoppingItems { get; set; }
}