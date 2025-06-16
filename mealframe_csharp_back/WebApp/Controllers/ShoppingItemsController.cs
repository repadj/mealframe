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
    public class ShoppingItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ShoppingItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ShoppingItems.Include(s => s.Product).Include(s => s.ShoppingList);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ShoppingItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingItem = await _context.ShoppingItems
                .Include(s => s.Product)
                .Include(s => s.ShoppingList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            return View(shoppingItem);
        }

        // GET: ShoppingItems/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "CreatedBy");
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingLists, "Id", "CreatedBy");
            return View();
        }

        // POST: ShoppingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Unit,ShoppingListId,ProductId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] ShoppingItem shoppingItem)
        {
            if (ModelState.IsValid)
            {
                shoppingItem.Id = Guid.NewGuid();
                _context.Add(shoppingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "CreatedBy", shoppingItem.ProductId);
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingLists, "Id", "CreatedBy", shoppingItem.ShoppingListId);
            return View(shoppingItem);
        }

        // GET: ShoppingItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingItem = await _context.ShoppingItems.FindAsync(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "CreatedBy", shoppingItem.ProductId);
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingLists, "Id", "CreatedBy", shoppingItem.ShoppingListId);
            return View(shoppingItem);
        }

        // POST: ShoppingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,Unit,ShoppingListId,ProductId,Id,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,SysNotes")] ShoppingItem shoppingItem)
        {
            if (id != shoppingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingItemExists(shoppingItem.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "CreatedBy", shoppingItem.ProductId);
            ViewData["ShoppingListId"] = new SelectList(_context.ShoppingLists, "Id", "CreatedBy", shoppingItem.ShoppingListId);
            return View(shoppingItem);
        }

        // GET: ShoppingItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingItem = await _context.ShoppingItems
                .Include(s => s.Product)
                .Include(s => s.ShoppingList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            return View(shoppingItem);
        }

        // POST: ShoppingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(id);
            if (shoppingItem != null)
            {
                _context.ShoppingItems.Remove(shoppingItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingItemExists(Guid id)
        {
            return _context.ShoppingItems.Any(e => e.Id == id);
        }
    }
}
