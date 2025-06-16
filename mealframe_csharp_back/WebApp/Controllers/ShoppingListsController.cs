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
    public class ShoppingListsController : Controller
    {
        private readonly AppDbContext _context;

        public ShoppingListsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingLists
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ShoppingLists.Include(s => s.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ShoppingLists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingList = await _context.ShoppingLists
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            return View(shoppingList);
        }

        // GET: ShoppingLists/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname");
            return View();
        }

        // POST: ShoppingLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SListName,UserId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] ShoppingList shoppingList)
        {
            if (ModelState.IsValid)
            {
                shoppingList.Id = Guid.NewGuid();
                _context.Add(shoppingList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", shoppingList.UserId);
            return View(shoppingList);
        }

        // GET: ShoppingLists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingList = await _context.ShoppingLists.FindAsync(id);
            if (shoppingList == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", shoppingList.UserId);
            return View(shoppingList);
        }

        // POST: ShoppingLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SListName,UserId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] ShoppingList shoppingList)
        {
            if (id != shoppingList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingListExists(shoppingList.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", shoppingList.UserId);
            return View(shoppingList);
        }

        // GET: ShoppingLists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingList = await _context.ShoppingLists
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            return View(shoppingList);
        }

        // POST: ShoppingLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shoppingList = await _context.ShoppingLists.FindAsync(id);
            if (shoppingList != null)
            {
                _context.ShoppingLists.Remove(shoppingList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingListExists(Guid id)
        {
            return _context.ShoppingLists.Any(e => e.Id == id);
        }
    }
}
