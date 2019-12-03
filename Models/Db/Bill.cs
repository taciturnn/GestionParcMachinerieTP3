using System;
using System.Collections.Generic;

namespace GestionParcMachinerieTP3.Models.Db
{
    public partial class Bill
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string Commands { get; set; }
        public bool Paid { get; set; }
        public int? Value { get; set; }

        public virtual Client Client { get; set; }
    }
}
