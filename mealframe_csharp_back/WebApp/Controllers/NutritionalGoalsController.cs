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
    public class NutritionalGoalsController : Controller
    {
        private readonly AppDbContext _context;

        public NutritionalGoalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: NutritionalGoals
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.NutritionalGoals.Include(n => n.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: NutritionalGoals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutritionalGoal = await _context.NutritionalGoals
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nutritionalGoal == null)
            {
                return NotFound();
            }

            return View(nutritionalGoal);
        }

        // GET: NutritionalGoals/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname");
            return View();
        }

        // POST: NutritionalGoals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalorieTarget,ProteinTarget,FatTarget,SugarTarget,CarbsTarget,SaltTarget,UserId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] NutritionalGoal nutritionalGoal)
        {
            if (ModelState.IsValid)
            {
                nutritionalGoal.Id = Guid.NewGuid();
                _context.Add(nutritionalGoal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", nutritionalGoal.UserId);
            return View(nutritionalGoal);
        }

        // GET: NutritionalGoals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutritionalGoal = await _context.NutritionalGoals.FindAsync(id);
            if (nutritionalGoal == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", nutritionalGoal.UserId);
            return View(nutritionalGoal);
        }

        // POST: NutritionalGoals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CalorieTarget,ProteinTarget,FatTarget,SugarTarget,CarbsTarget,SaltTarget,UserId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] NutritionalGoal nutritionalGoal)
        {
            if (id != nutritionalGoal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nutritionalGoal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NutritionalGoalExists(nutritionalGoal.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", nutritionalGoal.UserId);
            return View(nutritionalGoal);
        }

        // GET: NutritionalGoals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutritionalGoal = await _context.NutritionalGoals
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nutritionalGoal == null)
            {
                return NotFound();
            }

            return View(nutritionalGoal);
        }

        // POST: NutritionalGoals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var nutritionalGoal = await _context.NutritionalGoals.FindAsync(id);
            if (nutritionalGoal != null)
            {
                _context.NutritionalGoals.Remove(nutritionalGoal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NutritionalGoalExists(Guid id)
        {
            return _context.NutritionalGoals.Any(e => e.Id == id);
        }
    }
}
