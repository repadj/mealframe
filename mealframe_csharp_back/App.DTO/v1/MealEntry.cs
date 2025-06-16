using App.Domain;
using Base.Contracts;

namespace App.DTO.v1;

public class MealEntry : IDomainId
{
    public Guid Id { get; set; }
    
    public decimal Amount { get; set; }
    
    public EUnit Unit { get; set; }
    
    public EMealType MealType { get; set; }
    
    public Guid? RecipeId { get; set; }
    
    public Guid? ProductId { get; set; }
    
    public Guid MealPlanId { get; set; }
}