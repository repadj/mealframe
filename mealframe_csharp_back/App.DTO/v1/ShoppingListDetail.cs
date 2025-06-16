using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class ShoppingListDetail
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(32)]
    public string SListName { get; set; } = default!;
    
    public ICollection<ShoppingItemDetail>? ShoppingItems { get; set; }
}