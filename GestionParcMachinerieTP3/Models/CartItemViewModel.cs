using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionParcMachinerieTP3.Models
{
    public class CartItemViewModel
    {
        public CartItemViewModel() { }
        public CartItemViewModel(CartItem item, Machine machine, bool isValid)
        {
            Id = item.Id;
            ProductPrice = machine.RentPrice;
            ProductName = machine.Model;
            From = (long)item.From;
            To = (long)item.To;
            Valid = isValid;
        }

        public int Id { get; set; }
        public int ProductPrice { get; set; }
        public string ProductName { get; set; }
        public long From { get; set; }
        public long To { get; set; }
        public bool Valid { get; set; }
    }
}