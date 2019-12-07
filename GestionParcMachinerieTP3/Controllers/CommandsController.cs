using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionParcMachinerieTP3.DAL;
using GestionParcMachinerieTP3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GestionParcMachinerieTP3.Controllers
{
    [Authorize]
    public class CommandsController : Controller
    {
        private MachinerieContext db;

        public CommandsController()
        {
            this.db = new MachinerieContext();
        }

        public CommandsController(MachinerieContext db)
        {
            this.db = db;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Manage()
        {
            List<CommandViewModel> list = new List<CommandViewModel>();
            foreach (var command in db.Commands)
            {
                var machine = db.Machines.Find(command.MachineId);
                var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(command.UserId);
                list.Add(new CommandViewModel(command, machine, user.Email));
            }
            return View(list);
        }

        // GET: Commands
        public ActionResult Index()
        { 
            var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            List<CommandViewModel> list = new List<CommandViewModel>();
            foreach (var command in db.Commands.Where(s => (s.UserId == user.Id)))
            {
                    var machine = db.Machines.Find(command.MachineId);
                    list.Add(new CommandViewModel(command, machine, user.Email));
            }
            return View(list);
        }

        // GET: Commands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Command command = db.Commands.Find(id);
            if (command == null)
            {
                return HttpNotFound();
            }

            var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(command.UserId);
            List<CommandViewModel> list = new List<CommandViewModel>();
            var machine = db.Machines.Find(command.MachineId);
            return View(new CommandViewModel(command, machine, user.Email));
        }

        // GET: Commands/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.MachineId = new SelectList(db.Machines, "Id", "Model");
            return View();
        }

        // POST: Commands/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,MachineId,From,To,Status")] Command command)
        {
            if (ModelState.IsValid)
            {
                db.Commands.Add(command);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MachineId = new SelectList(db.Machines, "Id", "Model", command.MachineId);
            return View(command);
        }

        // GET: Commands/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Command command = db.Commands.Find(id);
            if (command == null)
            {
                return HttpNotFound();
            }
            ViewBag.MachineId = new SelectList(db.Machines, "Id", "Model", command.MachineId);
            return View(command);
        }

        // POST: Commands/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,MachineId,From,To,Status")] Command command)
        {
            if (ModelState.IsValid)
            {
                db.Entry(command).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage");
            }
            ViewBag.MachineId = new SelectList(db.Machines, "Id", "Model", command.MachineId);
            return View(command);
        }

        // GET: Commands/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Command command = db.Commands.Find(id);
            if (command == null)
            {
                return HttpNotFound();
            }
            return View(command);
        }

        // POST: Commands/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Command command = db.Commands.Find(id);
            db.Commands.Remove(command);
            db.SaveChanges();
            return RedirectToAction("Manage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
