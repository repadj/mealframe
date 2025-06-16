using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.DataSeeding;

public class AppDataInit
{
    public static void SeedAppData(AppDbContext context)
    {
    }

    public static void MigrateDatabase(AppDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DeleteDatabase(AppDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager)
    {
        foreach (var userInfo in InitialData.Users)
        {
            var user = userManager.FindByEmailAsync(userInfo.username).Result;
            if (user == null)
            {
                user = new AppUser()
                {
                    Id = userInfo.id ?? Guid.NewGuid(),
                    Email = userInfo.username,
                    UserName = userInfo.username,
                    EmailConfirmed = true,
                    Firstname = userInfo.firstName,
                    Lastname = userInfo.lastName,
                };
                var result = userManager.CreateAsync(user, userInfo.password).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }
            }
        }
    }
}