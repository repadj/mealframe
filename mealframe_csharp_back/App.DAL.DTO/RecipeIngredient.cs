using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class RecipeIngredient : IDomainId
{
    public Guid Id { get; set; }
    
    public decimal Amount { get; set; }
    
    public EUnit Unit { get; set; }
    
    public Guid RecipeId { get; set; }
    
    public Recipe? Recipe { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product? Product { get; set; }
}