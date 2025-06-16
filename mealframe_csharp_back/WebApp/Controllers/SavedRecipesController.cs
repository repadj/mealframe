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
    public class SavedRecipesController : Controller
    {
        private readonly AppDbContext _context;

        public SavedRecipesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SavedRecipes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SavedRecipes.Include(s => s.Recipe).Include(s => s.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SavedRecipes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedRecipe = await _context.SavedRecipes
                .Include(s => s.Recipe)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedRecipe == null)
            {
                return NotFound();
            }

            return View(savedRecipe);
        }

        // GET: SavedRecipes/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "CreatedBy");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname");
            return View();
        }

        // POST: SavedRecipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,UserId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] SavedRecipe savedRecipe)
        {
            if (ModelState.IsValid)
            {
                savedRecipe.Id = Guid.NewGuid();
                _context.Add(savedRecipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "CreatedBy", savedRecipe.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", savedRecipe.UserId);
            return View(savedRecipe);
        }

        // GET: SavedRecipes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedRecipe = await _context.SavedRecipes.FindAsync(id);
            if (savedRecipe == null)
            {
                return NotFound();
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "CreatedBy", savedRecipe.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", savedRecipe.UserId);
            return View(savedRecipe);
        }

        // POST: SavedRecipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RecipeId,UserId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] SavedRecipe savedRecipe)
        {
            if (id != savedRecipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(savedRecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavedRecipeExists(savedRecipe.Id))
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
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "CreatedBy", savedRecipe.RecipeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", savedRecipe.UserId);
            return View(savedRecipe);
        }

        // GET: SavedRecipes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedRecipe = await _context.SavedRecipes
                .Include(s => s.Recipe)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedRecipe == null)
            {
                return NotFound();
            }

            return View(savedRecipe);
        }

        // POST: SavedRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var savedRecipe = await _context.SavedRecipes.FindAsync(id);
            if (savedRecipe != null)
            {
                _context.SavedRecipes.Remove(savedRecipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavedRecipeExists(Guid id)
        {
            return _context.SavedRecipes.Any(e => e.Id == id);
        }
    }
}
