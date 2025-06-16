using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Contracts;

namespace APP.BLL.DTO;

public class RecipeIngredient : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Amount), ResourceType = typeof(App.Resources.Domain.RecipeIngredient))]
    public decimal Amount { get; set; }
    
    [Display(Name = nameof(Unit), ResourceType = typeof(App.Resources.Domain.RecipeIngredient))]
    public EUnit Unit { get; set; }
    
    public Guid RecipeId { get; set; }
    
    [Display(Name = nameof(Recipe), ResourceType = typeof(App.Resources.Domain.RecipeIngredient))]
    public Recipe? Recipe { get; set; }
    
    public Guid ProductId { get; set; }
    
    [Display(Name = nameof(Product), ResourceType = typeof(App.Resources.Domain.RecipeIngredient))]
    public Product? Product { get; set; }
}