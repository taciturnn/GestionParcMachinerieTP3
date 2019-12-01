using System;
using System.Collections.Generic;

namespace GestionParcMachinerieTP3.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Commands { get; set; }
        public bool Paid { get; set; }
        public int? Value { get; set; }
    }
}
