using System;
using System.Collections.Generic;

namespace GestionParcMachinerieTP3.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MachineId { get; set; }
        public long From { get; set; }
        public long To { get; set; }
    }
}
