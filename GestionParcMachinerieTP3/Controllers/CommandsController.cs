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
    public class CommandsController : Controller
    {
        private readonly Context context;

        public CommandsController(Context context)
        {
            this.context = context;
        }

        // GET: Commands
        public async Task<IActionResult> Index(DateTime from, DateTime to, string client, string machine, string status)
        {
            IQueryable<Command> query = context.Command;
            if (from != null) query = query.Where(s => s.From.GetValueOrDefault(0L) == from.ToBinary());
            if (to != null) query = query.Where(s => s.To.GetValueOrDefault(0L) == to.ToBinary());
            if (!String.IsNullOrEmpty(client))
            {
                query = query.Where(s => s.Client.Name.Contains(client));
            }
            if (!String.IsNullOrEmpty(machine))
            {
                query = query.Where(s => s.Machine.Model.Contains(machine));
            }
            if (!String.IsNullOrEmpty(status))
            {
                query = query.Where(s => s.Status.Contains(status));
            }
            return View(await query.Include(c => c.Client).Include(c => c.Machine).ToListAsync());
        }

        // GET: Commands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var command = await context.Command
                .Include(c => c.Client)
                .Include(c => c.Machine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (command == null)
            {
                return NotFound();
            }

            return View(command);
        }

        // GET: Commands/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(context.Client, "Id", "Id");
            ViewData["MachineId"] = new SelectList(context.Machine, "Id", "Model");
            return View();
        }

        // POST: Commands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,MachineId,From,To,Status")] Command command)
        {
            if (ModelState.IsValid)
            {
                context.Add(command);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(context.Client, "Id", "Id", command.ClientId);
            ViewData["MachineId"] = new SelectList(context.Machine, "Id", "Model", command.MachineId);
            return View(command);
        }

        // GET: Commands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var command = await context.Command.FindAsync(id);
            if (command == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(context.Client, "Id", "Id", command.ClientId);
            ViewData["MachineId"] = new SelectList(context.Machine, "Id", "Model", command.MachineId);
            return View(command);
        }

        // POST: Commands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,MachineId,From,To,Status")] Command command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(command);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandExists(command.Id))
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
            ViewData["ClientId"] = new SelectList(context.Client, "Id", "Id", command.ClientId);
            ViewData["MachineId"] = new SelectList(context.Machine, "Id", "Model", command.MachineId);
            return View(command);
        }

        // GET: Commands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var command = await context.Command
                .Include(c => c.Client)
                .Include(c => c.Machine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (command == null)
            {
                return NotFound();
            }

            return View(command);
        }

        // POST: Commands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var command = await context.Command.FindAsync(id);
            context.Command.Remove(command);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandExists(int id)
        {
            return context.Command.Any(e => e.Id == id);
        }
    }
}
