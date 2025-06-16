namespace App.DTO.v1;

public class GenerateShoppingListRequest
{
    public List<Guid> MealPlanIds { get; set; } = new();
}