namespace App.DTO.v1;

public class RecipeDetail
{
    public Guid Id { get; set; }
    public string RecipeName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int CookingTime { get; set; }
    public int Servings { get; set; }
    public string PictureUrl { get; set; } = default!;
    public bool Public { get; set; }

    public List<RecipeIngredientDetail> Ingredients { get; set; } = new();
}