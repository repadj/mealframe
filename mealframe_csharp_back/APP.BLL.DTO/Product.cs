using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace APP.BLL.DTO;

public class Product : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(48)] 
    [Display(Name = nameof(ProductName), ResourceType = typeof(App.Resources.Domain.Product))]
    public string ProductName { get; set; } = default!;
    
    [Display(Name = nameof(Calories), ResourceType = typeof(App.Resources.Domain.Product))]
    
    public int Calories { get; set; }
    
    [Display(Name = nameof(Protein), ResourceType = typeof(App.Resources.Domain.Product))]
    
    public decimal Protein { get; set; }
    
    [Display(Name = nameof(Carbs), ResourceType = typeof(App.Resources.Domain.Product))]
    
    public decimal Carbs { get; set; }
    
    [Display(Name = nameof(Fat), ResourceType = typeof(App.Resources.Domain.Product))]
    public decimal Fat { get; set; }
    
    [Display(Name = nameof(Sugar), ResourceType = typeof(App.Resources.Domain.Product))]
    public decimal Sugar { get; set; }
    
    [Display(Name = nameof(Salt), ResourceType = typeof(App.Resources.Domain.Product))]
    public decimal Salt { get; set; }
    
    public Guid CategoryId { get; set; }
    
    [Display(Name = nameof(Category), ResourceType = typeof(App.Resources.Domain.Product))]
    public Category? Category { get; set; }
    
    public ICollection<MealEntry>? MealEntries { get; set; }
    public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
    public ICollection<ShoppingItem>? ShoppingItems { get; set; }
}