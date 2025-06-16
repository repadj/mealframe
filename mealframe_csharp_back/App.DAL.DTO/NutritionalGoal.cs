using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Contracts;

namespace App.DAL.DTO;

public class NutritionalGoal : IDomainId
{
    public Guid Id { get; set; }
    
    public int CalorieTarget { get; set; }
    
    public decimal ProteinTarget { get; set; }
    
    public decimal FatTarget { get; set; }
    
    public decimal SugarTarget { get; set; }
    
    public decimal CarbsTarget { get; set; }
    
    public decimal SaltTarget { get; set; }
}