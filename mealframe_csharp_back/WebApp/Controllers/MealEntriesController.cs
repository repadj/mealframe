using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class MealEntriesController : Controller
    {
        private readonly AppDbContext _context;

        public MealEntriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MealEntries
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MealEntries.Include(m => m.MealPlan).Include(m => m.Product).Include(m => m.Recipe);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MealEntries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealEntry = await _context.MealEntries
                .Include(m => m.MealPlan)
                .Include(m => m.Product)
                .Include(m => m.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealEntry == null)
            {
                return NotFound();
            }

            return View(mealEntry);
        }

        // GET: MealEntries/Create
        public IActionResult Create()
        {
            ViewData["MealPlanId"] = new SelectList(_context.MealPlans, "Id", "CreatedBy");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "CreatedBy");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "CreatedBy");
            return View();
        }

        // POST: MealEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Unit,MealType,RecipeId,ProductId,MealPlanId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] MealEntry mealEntry)
        {
            if (ModelState.IsValid)
            {
                mealEntry.Id = Guid.NewGuid();
                _context.Add(mealEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealPlanId"] = new SelectList(_context.MealPlans, "Id", "CreatedBy", mealEntry.MealPlanId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "CreatedBy", mealEntry.ProductId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "CreatedBy", mealEntry.RecipeId);
            return View(mealEntry);
        }

        // GET: MealEntries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealEntry = await _context.MealEntries.FindAsync(id);
            if (mealEntry == null)
            {
                return NotFound();
            }
            ViewData["MealPlanId"] = new SelectList(_context.MealPlans, "Id", "CreatedBy", mealEntry.MealPlanId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "CreatedBy", mealEntry.ProductId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "CreatedBy", mealEntry.RecipeId);
            return View(mealEntry);
        }

        // POST: MealEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,Unit,MealType,RecipeId,ProductId,MealPlanId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] MealEntry mealEntry)
        {
            if (id != mealEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealEntryExists(mealEntry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealPlanId"] = new SelectList(_context.MealPlans, "Id", "CreatedBy", mealEntry.MealPlanId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "CreatedBy", mealEntry.ProductId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "CreatedBy", mealEntry.RecipeId);
            return View(mealEntry);
        }

        // GET: MealEntries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealEntry = await _context.MealEntries
                .Include(m => m.MealPlan)
                .Include(m => m.Product)
                .Include(m => m.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealEntry == null)
            {
                return NotFound();
            }

            return View(mealEntry);
        }

        // POST: MealEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mealEntry = await _context.MealEntries.FindAsync(id);
            if (mealEntry != null)
            {
                _context.MealEntries.Remove(mealEntry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealEntryExists(Guid id)
        {
            return _context.MealEntries.Any(e => e.Id == id);
        }
    }
}
