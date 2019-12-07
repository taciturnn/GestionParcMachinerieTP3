using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GestionParcMachinerieTP3.Models;
using GestionParcMachinerieTP3.DateTimeHelper;

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
            ProductDescription = machine.Description;
            From = DateTimeHelper.DateTimeHelper.LongToDateTime(item.From).ToString("MM/dd/yyyy");
            To = DateTimeHelper.DateTimeHelper.LongToDateTime(item.To).ToString("MM/dd/yyyy");
            Duration = DateTimeHelper.DateTimeHelper.LongDiff(item.From, item.To).Days + 1; // Same date -> diff = 0
            Cost = Duration * ProductPrice;
            Valid = isValid;
            ProductId = machine.Id;
        }

        public int Id { get; set; }
        [Display(Name = "Item Price (per day)")]
        public int ProductPrice { get; set; }
        [Display(Name = "Item")]
        public string ProductName { get; set; }
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [Display(Name = "Location Date (Start)")]
        public string From { get; set; }
        [Display(Name = "Location Date (End)")]
        public string To { get; set; }
        [Display(Name = "Location Duration")]
        public int Duration { get; set; }
        public bool Valid { get; set; }
        [Display(Name = "Cost for this location")]
        public int Cost { get; set; }
    }
}