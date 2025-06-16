using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class Product : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(48)] 
    public string ProductName { get; set; } = default!;
    
    [Required]
    public int Calories { get; set; }
    
    [Required]
    public decimal Protein { get; set; }
    
    [Required]
    public decimal Carbs { get; set; }
    
    [Required]
    public decimal Fat { get; set; }
    
    [Required]
    public decimal Sugar { get; set; }
    
    [Required]
    public decimal Salt { get; set; }
    
    public Guid CategoryId { get; set; }
}