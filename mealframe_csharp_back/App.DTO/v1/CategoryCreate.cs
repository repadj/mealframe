using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class CategoryCreate
{
    [Required]
    [MaxLength(128)]
    public string CategoryName { get; set; } = default!;
}