using App.BLL.Contracts;
using App.DAL.Contracts;
using App.DTO.v1;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;
using MealEntry = APP.BLL.DTO.MealEntry;
using MealPlan = APP.BLL.DTO.MealPlan;

namespace App.BLL.Services;

public class MealPlanService : BaseService<APP.BLL.DTO.MealPlan, App.DAL.DTO.MealPlan, App.DAL.Contracts.IMealPlanRepository>, IMealPlanService
{
    private readonly IAppUOW _uow;
    private readonly IRecipeService _recipeService;
    
    public MealPlanService(IAppUOW serviceUOW, IRecipeService recipeService,
        IMapper<MealPlan, DAL.DTO.MealPlan> bllMapper) : base(serviceUOW, serviceUOW.MealPlanRepository, bllMapper)
    {
        _uow = serviceUOW;
        _recipeService = recipeService;
    }
    
    public async Task<IEnumerable<APP.BLL.DTO.MealPlan>> GetAllWithEntriesAsync(Guid userId)
    {
        var dalMealPlans = await _uow.MealPlanRepository.GetAllDetailedAsync(userId);
        return dalMealPlans.Select(mp => Mapper.Map(mp))!;
    }
    
    public async Task<APP.BLL.DTO.MealPlan?> GetMealPlanWithEntriesAsync(Guid id, Guid userId)
    {
        var dalMealPlan = await _uow.MealPlanRepository.FirstOrDefaultDetailedAsync(id, userId);
        return Mapper.Map(dalMealPlan);
    }
    
    public async Task<MealPlan> UpdateWithEntriesAsync(MealPlan mealPlan, Guid userId)
    {
        var existingMealPlanDomain = await _uow.MealPlanRepository.FindDomainAsync(mealPlan.Id, userId);
        if (existingMealPlanDomain == null) throw new Exception("MealPlan not found");
        
        existingMealPlanDomain.PlanName = mealPlan.PlanName;
        existingMealPlanDomain.Date = mealPlan.Date;
        
        await _uow.MealEntryRepository.RemoveByMealPlanIdAsync(mealPlan.Id, userId);
        
        foreach (var entry in mealPlan.MealEntries ?? new List<MealEntry>())
        {
            var dalEntry = new DAL.DTO.MealEntry
            {
                Id = Guid.NewGuid(),
                MealPlanId = mealPlan.Id,
                ProductId = entry.ProductId,
                RecipeId = entry.RecipeId,
                Amount = entry.Amount,
                Unit = entry.Unit,
                MealType = entry.MealType
            };

            _uow.MealEntryRepository.Add(dalEntry, userId);
        }

        await _uow.SaveChangesAsync();
        return mealPlan;
    }

    
    public async Task<APP.BLL.DTO.MealPlan> CreateWithEntriesAsync(APP.BLL.DTO.MealPlan mealPlan, Guid userId)
    {
        if (mealPlan == null) throw new ArgumentNullException(nameof(mealPlan));
        
        var mappedMealPlan = Mapper.Map(mealPlan)!;
        mappedMealPlan.MealEntries = null;

        _uow.MealPlanRepository.Add(mappedMealPlan, userId);
        await _uow.SaveChangesAsync();
        
        foreach (var entry in mealPlan.MealEntries ?? new List<APP.BLL.DTO.MealEntry>())
        {
            var dalEntry = new DAL.DTO.MealEntry
            {
                Id = Guid.NewGuid(),
                MealPlanId = mappedMealPlan.Id,
                Amount = entry.Amount,
                Unit = entry.Unit,
                MealType = entry.MealType,
                ProductId = entry.ProductId,
                RecipeId = entry.RecipeId
            };

            _uow.MealEntryRepository.Add(dalEntry, userId);
        }

        await _uow.SaveChangesAsync();

        mealPlan.Id = mappedMealPlan.Id;
        return mealPlan;
    }
    
    public async Task RemoveWithEntriesAsync(Guid id, Guid userId)
    {
        await _uow.MealEntryRepository.RemoveByMealPlanIdAsync(id, userId);
        _uow.MealPlanRepository.Remove(id, userId);
        await _uow.SaveChangesAsync();
    }
    
    // BLL Service
    public async Task<APP.BLL.DTO.MealPlanMacroSummary?> GetMealPlanMacrosAsync(Guid mealPlanId, Guid userId)
    {
        var mealPlan = await _uow.MealPlanRepository.FirstOrDefaultDetailedAsync(mealPlanId, userId);
        if (mealPlan == null) return null;

        var macroSummary = new APP.BLL.DTO.MealPlanMacroSummary();

        foreach (var entry in mealPlan.MealEntries ?? new List<DAL.DTO.MealEntry>())
        {
            if (entry.Product != null)
            {
                var factor = entry.Amount / 100m;
                macroSummary.Calories += entry.Product.Calories * factor;
                macroSummary.Protein += entry.Product.Protein * factor;
                macroSummary.Carbs += entry.Product.Carbs * factor;
                macroSummary.Fat += entry.Product.Fat * factor;
                macroSummary.Sugar += entry.Product.Sugar * factor;
                macroSummary.Salt += entry.Product.Salt * factor;
            }
            else if (entry.Recipe != null)
            {
                var recipeMacros = await _recipeService.GetRecipeMacrosPerServingAsync(entry.Recipe.Id, userId);
                if (recipeMacros == null) continue;
                macroSummary.Calories += recipeMacros.Calories * entry.Amount;
                macroSummary.Protein += recipeMacros.Protein * entry.Amount;
                macroSummary.Carbs += recipeMacros.Carbs * entry.Amount;
                macroSummary.Fat += recipeMacros.Fat * entry.Amount;
                macroSummary.Sugar += recipeMacros.Sugar * entry.Amount;
                macroSummary.Salt += recipeMacros.Salt * entry.Amount;
            }
        }

        return macroSummary;
    }
}