using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Base.Helpers;

public static class IdentityExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userIdStr = claimsPrincipal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userId = Guid.Parse(userIdStr);
        return userId;
    }
    
    private static readonly JwtSecurityTokenHandler JWTSecurityTokenHandler = new JwtSecurityTokenHandler();
    
    public static string GenerateJwt(
        IEnumerable<Claim> claims,
        string key,
        string issuer,
        string audience,
        DateTime expires)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        
        var uniqueClaims = claims
            .GroupBy(c => new { c.Type, c.Value })
            .Select(g => g.First())
            .ToList();
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: uniqueClaims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        return JWTSecurityTokenHandler.WriteToken(token);
    }
    
    public static bool ValidateJWT(
        string token,
        string key,
        string issuer,
        string audience,
        bool ignoreExpiration = true
    )
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyBytes = Encoding.UTF8.GetBytes(key);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateLifetime = !ignoreExpiration, // ignore expiration if needed

            ClockSkew = TimeSpan.Zero // no time leeway
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }


}