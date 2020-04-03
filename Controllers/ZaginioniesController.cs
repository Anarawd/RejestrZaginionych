using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zaginionki.Data;
using Zaginionki.Models;

namespace Zaginionki.Controllers
{
    public class ZaginioniesController : Controller
    {
        private readonly ZaginionkiContext _context;

        public ZaginioniesController(ZaginionkiContext context)
        {
            _context = context;
        }

        // GET: Zaginionies
        public async Task<IActionResult> Index(string pleec, string searchString)
        {
            IQueryable<string> genderQuery = from m in _context.Zaginiony
                                             orderby m.Plec
                                             select m.Plec;
            var zaginieni = from m in _context.Zaginiony select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                zaginieni = zaginieni.Where(s => s.Wojewodztwo.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(pleec))
            {
                zaginieni = zaginieni.Where(s => s.Plec.Contains(pleec));
            }
            var genderVM = new ZaginionyPlecViewModel
            {
                Plec = new SelectList(await genderQuery.Distinct().ToListAsync()),
                Zaginieni = await zaginieni.ToListAsync()
            };
            return View(genderVM);
        }

        // GET: Zaginionies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaginiony = await _context.Zaginiony
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaginiony == null)
            {
                return NotFound();
            }

            return View(zaginiony);
        }

        // GET: Zaginionies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zaginionies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,DataZaginiecia,Wojewodztwo,Opis,Zdjecie")] Zaginiony zaginiony)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaginiony);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zaginiony);
        }

        // GET: Zaginionies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaginiony = await _context.Zaginiony.FindAsync(id);
            if (zaginiony == null)
            {
                return NotFound();
            }
            return View(zaginiony);
        }

        // POST: Zaginionies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,DataZaginiecia,Wojewodztwo,Opis,Zdjecie")] Zaginiony zaginiony)
        {
            if (id != zaginiony.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaginiony);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaginionyExists(zaginiony.Id))
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
            return View(zaginiony);
        }

        // GET: Zaginionies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaginiony = await _context.Zaginiony
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaginiony == null)
            {
                return NotFound();
            }

            return View(zaginiony);
        }

        // POST: Zaginionies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaginiony = await _context.Zaginiony.FindAsync(id);
            _context.Zaginiony.Remove(zaginiony);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaginionyExists(int id)
        {
            return _context.Zaginiony.Any(e => e.Id == id);
        }
    }
}
