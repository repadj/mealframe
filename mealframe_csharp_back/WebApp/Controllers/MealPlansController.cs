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
    public class MealPlansController : Controller
    {
        private readonly AppDbContext _context;

        public MealPlansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MealPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.MealPlans.ToListAsync());
        }

        // GET: MealPlans/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealPlan = await _context.MealPlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            return View(mealPlan);
        }

        // GET: MealPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MealPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanName,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] MealPlan mealPlan)
        {
            if (ModelState.IsValid)
            {
                mealPlan.Id = Guid.NewGuid();
                _context.Add(mealPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mealPlan);
        }

        // GET: MealPlans/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealPlan = await _context.MealPlans.FindAsync(id);
            if (mealPlan == null)
            {
                return NotFound();
            }
            return View(mealPlan);
        }

        // POST: MealPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PlanName,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] MealPlan mealPlan)
        {
            if (id != mealPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealPlanExists(mealPlan.Id))
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
            return View(mealPlan);
        }

        // GET: MealPlans/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealPlan = await _context.MealPlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            return View(mealPlan);
        }

        // POST: MealPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mealPlan = await _context.MealPlans.FindAsync(id);
            if (mealPlan != null)
            {
                _context.MealPlans.Remove(mealPlan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealPlanExists(Guid id)
        {
            return _context.MealPlans.Any(e => e.Id == id);
        }
    }
}
