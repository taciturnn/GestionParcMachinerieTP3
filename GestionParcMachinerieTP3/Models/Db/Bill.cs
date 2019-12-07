using System;
using System.Collections.Generic;

namespace GestionParcMachinerieTP3.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool Paid { get; set; }
        public int? Value { get; set; }

        public virtual AspNetUser User { get; set; }
        public virtual ICollection<BillCommand> BillCommands { get; set; }
    }
}
