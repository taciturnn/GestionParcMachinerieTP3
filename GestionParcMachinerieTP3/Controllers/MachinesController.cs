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
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace GestionParcMachinerieTP3.Controllers
{
    public class MachinesController : Controller
    {
        private MachinerieContext db;

        public MachinesController()
        {
            this.db = new MachinerieContext();
        }

        public MachinesController(MachinerieContext db)
        {
            this.db = db;
        }

        // GET: Machines
        public ActionResult Index(string filter, DateTime? from, DateTime? to)
        {
            IQueryable<Machine> query = db.Machines;
            if (!String.IsNullOrEmpty(filter))
            {
                query = query.Where(s => s.Model.Contains(filter));
            }

            long from_ = from.GetValueOrDefault(DateTime.Now).ToBinary();
            long to_ = to.GetValueOrDefault(DateTime.Now).ToBinary();
            List<int?> unavailable = db.Commands.Where(
                (s => (s.From >= from_ && s.From <= to_) || (s.To >= from_ && s.To <= to_))
            ).Select(s => s.MachineId).ToList();
            query = query.Where(s => !unavailable.Contains(s.Id));
            
            return View(query.ToList());
        }

        // GET: Machines
        [Authorize(Roles = "Admin")]
        public ActionResult Manage()
        {
            return View(db.Machines.ToList());
        }

        // GET: Machines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // GET: Machines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Model,RentPrice")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                db.Machines.Add(machine);
                db.SaveChanges();
                return RedirectToAction("Manage");
            }

            return View(machine);
        }

        // GET: Machines/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Model,RentPrice,Description")] Machine machine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manage");
            }
            return View(machine);
        }

        // GET: Machines/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine machine = db.Machines.Find(id);
            db.Machines.Remove(machine);
            db.SaveChanges();
            return RedirectToAction("Manage");
        }

        [Authorize]
        [HttpPost, ActionName("AddToCart")]
        public ActionResult AddToCart(int machineId, int from, int to)
        {
            var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            CartItem item = new CartItem();
            item.From = from;
            item.To = to;
            item.MachineId = machineId;
            item.UserId = user.Id;
            db.CartItems.Add(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
