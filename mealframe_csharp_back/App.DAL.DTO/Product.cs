using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DAL.DTO;

public class Product : IDomainId
{
    public Guid Id { get; set; }
    
    public string ProductName { get; set; } = default!;
    
    public int Calories { get; set; }
    
    public decimal Protein { get; set; }
    
    public decimal Carbs { get; set; }
    
    public decimal Fat { get; set; }
    
    public decimal Sugar { get; set; }
    
    public decimal Salt { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public ICollection<MealEntry>? MealEntries { get; set; }
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    public ICollection<ShoppingItem>? ShoppingItems { get; set; }
}