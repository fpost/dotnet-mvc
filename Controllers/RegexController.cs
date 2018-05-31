using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoginApp.Models;

namespace LoginApp.Controllers
{
    public class RegexController : Controller
    {
        private readonly LoginAppDbContext _context;

        public RegexController(LoginAppDbContext context)
        {
            _context = context;
        }

        // GET: Regex
        public async Task<IActionResult> Index()
        {
            return View(await _context.Regex.ToListAsync());
        }

        // GET: Regex/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regex = await _context.Regex
                .SingleOrDefaultAsync(m => m.ID == id);
            if (regex == null)
            {
                return NotFound();
            }

            return View(regex);
        }

        // GET: Regex/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regex/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Value")] Regex regex)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regex);
        }

        // GET: Regex/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regex = await _context.Regex.SingleOrDefaultAsync(m => m.ID == id);
            if (regex == null)
            {
                return NotFound();
            }
            return View(regex);
        }

        // POST: Regex/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Value")] Regex regex)
        {
            if (id != regex.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegexExists(regex.ID))
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
            return View(regex);
        }

        // GET: Regex/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regex = await _context.Regex
                .SingleOrDefaultAsync(m => m.ID == id);
            if (regex == null)
            {
                return NotFound();
            }

            return View(regex);
        }

        // POST: Regex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regex = await _context.Regex.SingleOrDefaultAsync(m => m.ID == id);
            _context.Regex.Remove(regex);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegexExists(int id)
        {
            return _context.Regex.Any(e => e.ID == id);
        }
    }
}
