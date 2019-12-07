using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using GestionParcMachinerieTP3.DAL;
using GestionParcMachinerieTP3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

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
            var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            List<CartItemViewModel> list = new List<CartItemViewModel>();
            foreach (var cartItem in db.CartItems)
            {
                if (cartItem.UserId == user.Id)
                {
                    var machine = db.Machines.Find(cartItem.MachineId);
                    list.Add(new CartItemViewModel(cartItem, machine, validate(machine, cartItem.From, cartItem.To)));
                }
            }
            return View(list);
        }

        [Authorize]
        [HttpPost, ActionName("AddToCommands")]
        public ActionResult AddToCommands(int cartId)
        {
            var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            Command item = new Command();
            CartItem itemSupr = db.CartItems.Find(cartId);
            long cost = 0;
            foreach (var cartItem in db.CartItems)
            {
                if (cartItem.Id == cartId)
                {

                    long Duration = DateTimeHelper.DateTimeHelper.LongDiff(cartItem.From, cartItem.To).Days + 1; // Same date -> diff = 0
                    Machine machine = db.Machines.Find(cartItem.MachineId);
                    cost += machine.RentPrice * Duration;

                    item.From = cartItem.From;
                    item.To = cartItem.To ;
                    item.MachineId = cartItem.MachineId ;
                    item.UserId = user.Id;
                    item.Status = null;
                    break;
                }
            }
            
            // New command
            var newCommandId = db.Commands.Add(item).Id;
            db.CartItems.Remove(itemSupr);

            // Create bill
            Bill bill = new Bill();
            bill.UserId = user.Id;
            bill.Value = (int)cost;
            var newBillId = db.Bills.Add(bill).Id;

            // Add commands to bill
            BillCommand billCommand = new BillCommand();
            billCommand.BillId = newBillId;
            billCommand.CommandId = newCommandId;
            db.BillCommands.Add(billCommand);

            // Save
            db.SaveChanges();
            return View("AddToCommands");
        }

        [Authorize]
        [HttpPost, ActionName("AddAllToCommands")]
        public ActionResult AddAllToCommands()
        {
            var user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            CartItem itemSupr = new CartItem();
            List<int> commandIds = new List<int>();
            long cost = 0;
            var cartItems = db.CartItems.ToList();
            for(int i = 0; i < cartItems.Count(); i++)
            {
                var cartItem = cartItems[i];
                if (cartItem.UserId == user.Id)
                {
                    Command item = new Command();

                    long Duration = DateTimeHelper.DateTimeHelper.LongDiff(cartItem.From, cartItem.To).Days + 1; // Same date -> diff = 0
                    Machine machine = db.Machines.Find(cartItem.MachineId);
                    cost += machine.RentPrice * Duration;

                    itemSupr = db.CartItems.Find(cartItem.Id);

                    item.From = cartItem.From;
                    item.To = cartItem.To;
                    item.MachineId = cartItem.MachineId;
                    item.UserId = user.Id;
                    item.Status = null;

                    db.Commands.Add(item);
                    db.CartItems.Remove(itemSupr);
                    db.SaveChanges();

                    commandIds.Add(item.Id);
                }
            }

            // Create bill
            Bill bill = new Bill();
            bill.UserId = user.Id;
            bill.Value = (int)cost;
            db.Bills.Add(bill);
            db.SaveChanges();

            foreach (int commandId in commandIds)
            {
                // Add commands to bill
                BillCommand billCommand = new BillCommand();
                billCommand.BillId = bill.Id;
                billCommand.CommandId = commandId;
                db.BillCommands.Add(billCommand);
                db.SaveChanges();
            }

            return View("AddToCommands");
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