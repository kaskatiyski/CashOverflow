using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CashOverflow.Models;
using CashOverflow.Web.Data;

namespace CashOverflow.Web.Controllers
{
    public class RecurringPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecurringPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RecurringPayments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RecurringPayments.Include(r => r.Category).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RecurringPayments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurringPayment = await _context.RecurringPayments
                .Include(r => r.Category)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recurringPayment == null)
            {
                return NotFound();
            }

            return View(recurringPayment);
        }

        // GET: RecurringPayments/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: RecurringPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,StartDate,Payments,Interval,Period,CategoryId,UserId")] RecurringPayment recurringPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recurringPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", recurringPayment.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", recurringPayment.UserId);
            return View(recurringPayment);
        }

        // GET: RecurringPayments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurringPayment = await _context.RecurringPayments.FindAsync(id);
            if (recurringPayment == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", recurringPayment.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", recurringPayment.UserId);
            return View(recurringPayment);
        }

        // POST: RecurringPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Amount,StartDate,Payments,Interval,Period,CategoryId,UserId")] RecurringPayment recurringPayment)
        {
            if (id != recurringPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recurringPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecurringPaymentExists(recurringPayment.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", recurringPayment.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", recurringPayment.UserId);
            return View(recurringPayment);
        }

        // GET: RecurringPayments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recurringPayment = await _context.RecurringPayments
                .Include(r => r.Category)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recurringPayment == null)
            {
                return NotFound();
            }

            return View(recurringPayment);
        }

        // POST: RecurringPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var recurringPayment = await _context.RecurringPayments.FindAsync(id);
            _context.RecurringPayments.Remove(recurringPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecurringPaymentExists(string id)
        {
            return _context.RecurringPayments.Any(e => e.Id == id);
        }
    }
}
