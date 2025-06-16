using App.Domain;
using App.Domain.Identity;
using Base.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>

{
    public DbSet<MealPlan> MealPlans { get; set; } = default!;
    public DbSet<MealEntry> MealEntries { get; set; } = default!;
    public DbSet<ShoppingList> ShoppingLists { get; set; } = default!;
    public DbSet<ShoppingItem> ShoppingItems { get; set; } = default!;
    public DbSet<SavedRecipe> SavedRecipes { get; set; } = default!;
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = default!;
    public DbSet<Recipe> Recipes { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<NutritionalGoal> NutritionalGoals { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;
    
    private readonly IUserNameResolver _userNameResolver;
    private readonly ILogger<AppDbContext> _logger;
    
    public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameResolver userNameResolver, ILogger<AppDbContext> logger)
        : base(options)
    {
        _userNameResolver = userNameResolver;
        _logger = logger;
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<MealEntry>().Property(e => e.Unit)
            .HasConversion<string>();
        builder.Entity<MealEntry>().Property(e => e.MealType)
            .HasConversion<string>();
        
        builder.Entity<ShoppingItem>().Property(e => e.Unit)
            .HasConversion<string>();
        
        builder.Entity<RecipeIngredient>().Property(e => e.Unit)
            .HasConversion<string>();

        // remove cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // We have custom UserRole - with separate PK and navigation for Role and User
        // override default Identity EF config
        builder.Entity<AppUserRole>().HasKey(a => a.Id);

        builder.Entity<AppUserRole>().HasIndex(a => new { a.UserId, a.RoleId }).IsUnique();
            
        builder.Entity<AppUserRole>()
            .HasOne(a => a.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(a => a.UserId);

        builder.Entity<AppUserRole>()
            .HasOne(a => a.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(a => a.RoleId);
        
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IDomainMeta);

        foreach (var entry in entries)
        {
            var entity = (IDomainMeta)entry.Entity;
            switch (entry.State)
            {
                case EntityState.Added:
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = _userNameResolver.CurrentUserName;
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = _userNameResolver.CurrentUserName;
                    break;

                case EntityState.Modified:
                    entry.Property(nameof(IDomainMeta.CreatedAt)).IsModified = false;
                    entry.Property(nameof(IDomainMeta.CreatedBy)).IsModified = false;

                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = _userNameResolver.CurrentUserName;
                    break;
            }

            if (entry is { Entity: IDomainUserId, State: EntityState.Modified })
            {
                entry.Property("UserId").IsModified = false;
                _logger.LogWarning("UserId modification attempt. Denied!");
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }

}