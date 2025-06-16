using System.Net;
using System.Net.Http.Json;
using App.DTO.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WebApp.ApiControllers.Identity;

namespace App.Tests.Integration.Api;

[Collection("Sequential")]
public class IdentityTests: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;


    public IdentityTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    
    [Fact]
    public async Task Register_New_User()
    {
        // Arrange
        var registrationData = new RegisterInfo()
        {
            Email = "test@test.ee",
            FirstName = "Test",
            LastName = "User",
            Password = "Password.123"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/register", registrationData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
    }


    [Fact]
    public async Task Login_Existing_User()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "test@account.com",
            Password = "Testing123@"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
    }

    [Fact]
    public async Task Login_Existing_User_Check_Rights()
    {
        // Arrange
        var loginData = new LoginInfo()
        {
            Email = "test@account.com",
            Password = "Testing123@"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);
        
        var getResponse = await _client.GetAsync("/api/v1/products");
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task No_Bearer_Header_Unauthorized()
    {
        var getResponse = await _client.GetAsync("/api/v1/products");
        Assert.Equal(HttpStatusCode.Unauthorized, getResponse.StatusCode);
    }
    
    [Fact]
    public async Task JWT_Custom_Expiration()
    {
        var loginData = new LoginInfo()
        {
            Email = "test@account.com",
            Password = "Testing123@"
        };
        
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=1", loginData);
        
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);
        
        var getResponse = await _client.GetAsync("/api/v1/products");
        getResponse.EnsureSuccessStatusCode();

        
        // Wait for JWT to expire
        await Task.Delay(1500);
        var getResponseAuthExpired = await _client.GetAsync("/api/v1/products");
        
        Assert.Equal(HttpStatusCode.Unauthorized, getResponseAuthExpired.StatusCode);
    }


    [Fact]
    public async Task JWT_Refresh()
    {
        var loginData = new LoginInfo()
        {
            Email = "test@account.com",
            Password = "Testing123@"
        };
        
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=1", loginData);
        
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(responseData);
        Assert.True(responseData.JWT.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.JWT);
        
        var getResponse = await _client.GetAsync("/api/v1/products");
        getResponse.EnsureSuccessStatusCode();
        
        await Task.Delay(1500);

        var getResponseAuthExpired = await _client.GetAsync("/api/v1/products");
        
        Assert.Equal(HttpStatusCode.Unauthorized, getResponseAuthExpired.StatusCode);
        
        var refreshResponse = await _client.PostAsJsonAsync("/api/v1/account/RenewRefreshToken", new TokenRefreshInfo()
        {
            Jwt = responseData.JWT,
            RefreshToken = responseData.RefreshToken
        });
        
        var refreshedResponseData = await refreshResponse.Content.ReadFromJsonAsync<JWTResponse>();
        Assert.NotNull(refreshedResponseData);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", refreshedResponseData.JWT);
        
        var getResponse2 = await _client.GetAsync("/api/v1/products");
        getResponse2.EnsureSuccessStatusCode();
    }
    
    
}