using System.ComponentModel.DataAnnotations;
using Base.Contracts;

namespace App.DAL.DTO;

public class SavedRecipe : IDomainId
{
    public Guid Id { get; set; }
    
    public Guid RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}