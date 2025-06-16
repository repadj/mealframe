using Moq;
using App.BLL.Services;
using App.DAL.Contracts;
using App.Domain;
using Base.Contracts;
using FluentAssertions;
using Recipe = APP.BLL.DTO.Recipe;
using RecipeIngredient = APP.BLL.DTO.RecipeIngredient;

namespace App.Tests.Unit;

public class RecipeServiceTests
{
    private readonly Mock<IAppUOW> _uowMock;
    private readonly Mock<IRecipeRepository> _recipeRepoMock;
    private readonly Mock<IRecipeIngredientRepository> _ingredientRepoMock;
    private readonly Mock<IMapper<Recipe, App.DAL.DTO.Recipe>> _mapperMock;
    private readonly RecipeService _service;

    public RecipeServiceTests()
    {
        _uowMock = new Mock<IAppUOW>();
        _recipeRepoMock = new Mock<IRecipeRepository>();
        _ingredientRepoMock = new Mock<IRecipeIngredientRepository>();
        _mapperMock = new Mock<IMapper<Recipe, App.DAL.DTO.Recipe>>();

        // Setup the IAppUOW to return the mocked repositories
        _uowMock.Setup(u => u.RecipeRepository).Returns(_recipeRepoMock.Object);
        _uowMock.Setup(u => u.RecipeIngredientRepository).Returns(_ingredientRepoMock.Object);

        _service = new RecipeService(_uowMock.Object, _mapperMock.Object);
    }

    // Sample test - GetDetailedAsync
    [Fact]
    public async Task GetDetailedAsync_ReturnsMappedRecipe()
    {
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dalRecipe = new App.DAL.DTO.Recipe { Id = recipeId, RecipeName = "Pasta" };
        var bllRecipe = new Recipe { Id = recipeId, RecipeName = "Pasta" };

        _recipeRepoMock.Setup(r => r.FirstOrDefaultDetailedAsync(recipeId, userId)).ReturnsAsync(dalRecipe);
        _mapperMock.Setup(m => m.Map(dalRecipe)).Returns(bllRecipe);

        var result = await _service.GetDetailedAsync(recipeId, userId);

        result.Should().NotBeNull();
        result.RecipeName.Should().Be("Pasta");
    }

    [Fact]
    public async Task CreateWithIngredientsAsync_AddsRecipeAndIngredients()
    {
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
    
        var bllRecipe = new Recipe
        {
            Id = recipeId,
            RecipeName = "Pasta",
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { ProductId = Guid.NewGuid(), Amount = 100, Unit = EUnit.Grams }
            }
        };
    
        var dalRecipe = new App.DAL.DTO.Recipe { Id = recipeId, RecipeName = "Pasta" };
    
        _mapperMock.Setup(m => m.Map(bllRecipe)).Returns(dalRecipe);
    
        _recipeRepoMock.Setup(r => r.Add(dalRecipe, userId));
        _uowMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

        var result = await _service.CreateWithIngredientsAsync(bllRecipe, userId);

        _recipeRepoMock.Verify(r => r.Add(dalRecipe, userId), Times.Once);
        _ingredientRepoMock.Verify(r => r.Add(It.IsAny<App.DAL.DTO.RecipeIngredient>(), It.IsAny<Guid>()), Times.Exactly(bllRecipe.RecipeIngredients.Count));
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Exactly(2));

        result.Should().NotBeNull();
        result.Id.Should().Be(recipeId);
    }
    
    [Fact]
    public async Task UpdateWithIngredientsAsync_UpdatesRecipeAndReplacesIngredients()
    {
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
    
        var bllRecipe = new Recipe
        {
            Id = recipeId,
            RecipeName = "Updated Pasta",
            RecipeIngredients = new List<RecipeIngredient>
            {
                new RecipeIngredient { ProductId = Guid.NewGuid(), Amount = 150, Unit = EUnit.Grams }
            }
        };
    
        var dalRecipe = new App.DAL.DTO.Recipe { Id = recipeId, RecipeName = "Updated Pasta" };
    
        _mapperMock.Setup(m => m.Map(bllRecipe)).Returns(dalRecipe);
    
        _ingredientRepoMock.Setup(r => r.RemoveByRecipeIdAsync(recipeId)).Returns(Task.CompletedTask);
        _uowMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

        var result = await _service.UpdateWithIngredientsAsync(bllRecipe, userId);

        _ingredientRepoMock.Verify(r => r.RemoveByRecipeIdAsync(recipeId), Times.Once);
        _recipeRepoMock.Verify(r => r.Update(dalRecipe, It.IsAny<Guid>()), Times.Once);
        _ingredientRepoMock.Verify(r => r.Add(It.IsAny<App.DAL.DTO.RecipeIngredient>(), It.IsAny<Guid>()), Times.Exactly(bllRecipe.RecipeIngredients.Count));
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Exactly(2));

        result.Should().NotBeNull();
        result.Id.Should().Be(recipeId);
    }
    
    [Fact]
    public async Task RemoveRecipeWithIngredientsAsync_RemovesIngredientsAndRecipe()
    {
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _ingredientRepoMock.Setup(r => r.RemoveByRecipeIdAsync(recipeId)).Returns(Task.CompletedTask);
        _uowMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
        _recipeRepoMock.Setup(r => r.FindAsync(recipeId, userId)).ReturnsAsync(new App.DAL.DTO.Recipe { Id = recipeId });

        // Act
        await _service.RemoveRecipeWithIngredientsAsync(recipeId, userId);

        // Assert
        _ingredientRepoMock.Verify(r => r.RemoveByRecipeIdAsync(recipeId), Times.Once);
        _recipeRepoMock.Verify(r => r.FindAsync(recipeId, userId), Times.Once);
        _recipeRepoMock.Verify(r => r.RemoveAsync(recipeId, userId), Times.Once);
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Exactly(1)); // Only once for the base RemoveAsync
    }
    
}
