using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class ShoppingListCreate
{
    [Required]
    [MaxLength(32)]
    public string SListName { get; set; } = default!;
}