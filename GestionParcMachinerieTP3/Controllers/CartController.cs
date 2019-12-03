using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using GestionParcMachinerieTP3.DAL;
using GestionParcMachinerieTP3.Models;

namespace GestionParcMachinerieTP3.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private MachinerieContext db = new MachinerieContext();

        public CartController()
        {
            this.db = new MachinerieContext();
        }

        public CartController(MachinerieContext db)
        {
            this.db = db;
        }

        // GET: Cart
        public ActionResult Index()
        {
            List<CartItemViewModel> list = new List<CartItemViewModel>();
            foreach (var cartItem in db.CartItems)
            {
                var machine = db.Machines.Find(cartItem.MachineId);
                list.Add(new CartItemViewModel(cartItem, machine, validate(machine, cartItem.From, cartItem.To)));
            }
            return View(list);
        }

        // GET: Machines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            var machine = db.Machines.Find(cartItem.MachineId);
            return View(new CartItemViewModel(cartItem, machine, validate(machine, cartItem.From, cartItem.To)));
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem item = db.CartItems.Find(id);
            db.CartItems.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public bool validate(Machine machine, long from, long to)
        {
            foreach (var command in db.Commands.Where(p => p.MachineId == machine.Id))
            {
                var tmp_2 = command.To >= to;
                if ((command.From >= from && command.From <= to) || // A command already reserves the machine starting in the requested range
                     (command.From <= from && command.To >= to) ||  // A command already reserves the machine during the requested range
                     (command.To >= from && command.To <= to))      // A command already reserves the machine ending in the requested range 
                {
                    return false;
                }
            }
            return true;
        }
    }
}