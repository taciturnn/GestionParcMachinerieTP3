using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionParcMachinerieTP3.Models.Db;

namespace GestionParcMachinerieTP3.Controllers
{
    public class MachinesController : Controller
    {
        private readonly Context context;

        public MachinesController(Context context)
        {
            this.context = context;
        }

        // GET: Machines
        public async Task<IActionResult> Index(string filter)
        {
            IQueryable<Machine> query = context.Machine;
            if (!String.IsNullOrEmpty(filter))
            {
                query = query.Where(s => s.Model.Contains(filter));
            }
            return View(await query.ToListAsync());
        }

        // GET: Machines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machine = await context.Machine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machine == null)
            {
                return NotFound();
            }

            return View(machine);
        }

        // GET: Machines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Machines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,RentPrice")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                context.Add(machine);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machine);
        }

        // GET: Machines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machine = await context.Machine.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }
            return View(machine);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,RentPrice")] Machine machine)
        {
            if (id != machine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(machine);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineExists(machine.Id))
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
            return View(machine);
        }

        // GET: Machines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machine = await context.Machine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machine == null)
            {
                return NotFound();
            }

            return View(machine);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machine = await context.Machine.FindAsync(id);
            context.Machine.Remove(machine);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachineExists(int id)
        {
            return context.Machine.Any(e => e.Id == id);
        }
    }
}
