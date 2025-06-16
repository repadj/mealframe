using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class NutritionalGoalCreate
{
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