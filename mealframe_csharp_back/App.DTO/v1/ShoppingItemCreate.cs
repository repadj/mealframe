using System.ComponentModel.DataAnnotations;
using App.Domain;

namespace App.DTO.v1;

public class ShoppingItemCreate
{
    [Required]
    public decimal Amount { get; set; }
    
    public EUnit Unit { get; set; }
    
    public Guid ShoppingListId { get; set; }
    
    public Guid ProductId { get; set; }
}