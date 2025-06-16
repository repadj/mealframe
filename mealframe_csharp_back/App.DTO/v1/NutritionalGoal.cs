using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DTO.v1;

public class NutritionalGoal : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    public int CalorieTarget { get; set; }
    
    [Required]
    public decimal ProteinTarget { get; set; }
    
    [Required]
    public decimal FatTarget { get; set; }
    
    [Required]
    public decimal SugarTarget { get; set; }

    [Required]
    public decimal CarbsTarget { get; set; }
    
    [Required]
    public decimal SaltTarget { get; set; }
}