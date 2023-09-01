using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KorsatkoApp.Data;
using KorsatkoApp.Models;

namespace KorsatkoApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnrolmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrolmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Enrolments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Enrolments.Include(e => e.session).Include(e => e.student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Enrolments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enrolments == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolments
                .Include(e => e.session)
                .Include(e => e.student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (enrolment == null)
            {
                return NotFound();
            }

            return View(enrolment);
        }

        // GET: Admin/Enrolments/Create
        public IActionResult Create()
        {
            ViewData["Sessions"] = new SelectList(_context.Sessions, "Id", "Id");
            ViewData["Students"] = new SelectList(_context.Students, "Id", "Email");
            return View();
        }

        // POST: Admin/Enrolments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,SessionId,PaymentStatus,AddedOn")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrolment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", enrolment.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", enrolment.StudentId);
            return View(enrolment);
        }

        // GET: Admin/Enrolments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrolments == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolments.FindAsync(id);
            if (enrolment == null)
            {
                return NotFound();
            }
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", enrolment.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", enrolment.StudentId);
            return View(enrolment);
        }

        // POST: Admin/Enrolments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,SessionId,PaymentStatus,AddedOn")] Enrolment enrolment)
        {
            if (id != enrolment.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrolment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrolmentExists(enrolment.StudentId))
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
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", enrolment.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Email", enrolment.StudentId);
            return View(enrolment);
        }

        // GET: Admin/Enrolments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrolments == null)
            {
                return NotFound();
            }

            var enrolment = await _context.Enrolments
                .Include(e => e.session)
                .Include(e => e.student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (enrolment == null)
            {
                return NotFound();
            }

            return View(enrolment);
        }

        // POST: Admin/Enrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrolments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Enrolments'  is null.");
            }
            var enrolment = await _context.Enrolments.FindAsync(id);
            if (enrolment != null)
            {
                _context.Enrolments.Remove(enrolment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrolmentExists(int id)
        {
          return (_context.Enrolments?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
