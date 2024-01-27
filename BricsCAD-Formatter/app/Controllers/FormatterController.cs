using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app.Models;

namespace app.Controllers
{
    public class FormatterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormatterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Formatter
        public async Task<IActionResult> Index()
        {
              return _context.FormattedCoordsModel != null ? 
                          View(await _context.FormattedCoordsModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FormattedCoordsModel'  is null.");
        }

        // GET: Formatter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FormattedCoordsModel == null)
            {
                return NotFound();
            }

            var formattedCoordsModel = await _context.FormattedCoordsModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (formattedCoordsModel == null)
            {
                return NotFound();
            }

            return View(formattedCoordsModel);
        }

        // GET: Formatter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formatter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,coordInput,formattedCoord")] FormattedCoordsModel formattedCoordsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formattedCoordsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formattedCoordsModel);
        }

        // GET: Formatter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FormattedCoordsModel == null)
            {
                return NotFound();
            }

            var formattedCoordsModel = await _context.FormattedCoordsModel.FindAsync(id);
            if (formattedCoordsModel == null)
            {
                return NotFound();
            }
            return View(formattedCoordsModel);
        }

        // POST: Formatter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,coordInput,formattedCoord")] FormattedCoordsModel formattedCoordsModel)
        {
            if (id != formattedCoordsModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formattedCoordsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormattedCoordsModelExists(formattedCoordsModel.id))
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
            return View(formattedCoordsModel);
        }

        // GET: Formatter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FormattedCoordsModel == null)
            {
                return NotFound();
            }

            var formattedCoordsModel = await _context.FormattedCoordsModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (formattedCoordsModel == null)
            {
                return NotFound();
            }

            return View(formattedCoordsModel);
        }

        // POST: Formatter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FormattedCoordsModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FormattedCoordsModel'  is null.");
            }
            var formattedCoordsModel = await _context.FormattedCoordsModel.FindAsync(id);
            if (formattedCoordsModel != null)
            {
                _context.FormattedCoordsModel.Remove(formattedCoordsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormattedCoordsModelExists(int id)
        {
          return (_context.FormattedCoordsModel?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
