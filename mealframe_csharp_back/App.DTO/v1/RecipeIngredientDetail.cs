namespace App.DTO.v1;

public class RecipeIngredientDetail
{
    public decimal Amount { get; set; }
    public string Unit { get; set; } = default!;
    
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
}