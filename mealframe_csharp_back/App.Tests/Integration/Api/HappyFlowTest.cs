using App.DAL.EF;
using App.DTO.v1;
using WebApp.ApiControllers.Identity;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace App.Tests.Integration.Api;

public class HappyFlowTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _client;

    public HappyFlowTest(CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient();

        using var scope = factory.Services.CreateScope();

        AuthenticateTestUser().GetAwaiter().GetResult();
    }
    
    
    [Fact]
    public async Task MealPlan_HappyPath_ShouldSucceed()
    {
        var categoryId = await SeedCategory("Protein");
        
        var productResponse = await _client.PostAsJsonAsync("/api/v1/products", new
        {
            productName = "Chicken Breast",
            calories = 165,
            protein = 31,
            carbs = 0,
            fat = 3.6m,
            sugar = 0,
            salt = 0.1m,
            categoryId,
        });
        
        productResponse.EnsureSuccessStatusCode();
        var product = await productResponse.Content.ReadFromJsonAsync<Product>();

        
        var recipeResponse = await _client.PostAsJsonAsync("/api/v1/recipes", new
        {
            RecipeName = "Grilled Chicken",
            Description = "Grill some chicken",
            CookingTime = 25,
            Servings = 2,
            PictureUrl = "http://example.com/chicken.jpg",
            Public = true,
            Ingredients = new[]
            {
                new { ProductId = product!.Id, Amount = 200, Unit = "Grams" }
            }
        });
        
        productResponse.EnsureSuccessStatusCode();
        var recipe = await recipeResponse.Content.ReadFromJsonAsync<RecipeDetail>();
        
        
        var mealPlanResponse = await _client.PostAsJsonAsync("/api/v1/mealplans", new
        {
            planName = "Weekly Plan",
            date = DateOnly.FromDateTime(DateTime.UtcNow).ToString("yyyy-MM-dd"),
            mealEntries = new[]
            {
                new { amount = 1, unit = "Servings", mealType = "Lunch", recipeId = recipe!.Id }
            }
        });
        
        mealPlanResponse.EnsureSuccessStatusCode();
        var mealPlan = await mealPlanResponse.Content.ReadFromJsonAsync<MealPlanDetail>();
        
        mealPlan.Should().NotBeNull();
        mealPlan!.MealEntries.Should().ContainSingle();
        mealPlan.MealEntries[0].RecipeId.Should().Be(recipe.Id);
    }
    
    private async Task AuthenticateTestUser()
    {
        var registerResponse = await _client.PostAsJsonAsync("/api/v1/account/register", new
        {
            Email = "test@user.com",
            Firstname = "TestUser",
            Lastname = "TestUser",
            Password = "Testing123@"
        });
        registerResponse.EnsureSuccessStatusCode();
    
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/account/login", new
        {
            Email = "test@user.com",
            Password = "Testing123@"
        });
        loginResponse.EnsureSuccessStatusCode();
    
        var jwt = (await loginResponse.Content.ReadFromJsonAsync<JWTResponse>())!.JWT;
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }

    private async Task<Guid> SeedCategory(string name)
    {
        var response = await _client.PostAsJsonAsync("/api/v1/categories", new { categoryName = name });
        response.EnsureSuccessStatusCode();
        var category = await response.Content.ReadFromJsonAsync<Category>();
        return category!.Id;
    }
}
