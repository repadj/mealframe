using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class ShoppingList : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(32)]
    public string SListName { get; set; } = default!;
}