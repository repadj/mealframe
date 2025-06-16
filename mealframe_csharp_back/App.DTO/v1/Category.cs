using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class Category : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string CategoryName { get; set; } = default!;
}