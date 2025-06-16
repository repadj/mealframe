using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Contracts;

namespace App.DTO.v1;

public class RecipeIngredient : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    public EUnit Unit { get; set; }
    
    public Guid RecipeId { get; set; }
    
    public Guid ProductId { get; set; }
    
}