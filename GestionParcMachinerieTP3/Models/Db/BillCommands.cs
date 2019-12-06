using System;
using System.Collections.Generic;

namespace GestionParcMachinerieTP3.Models
{
    public partial class BillCommands
    {
        public int Id { get; set; }
        public int CommandId { get; set; }
        public int BillId { get; set; }

        public virtual Command Command { get; set; }
        public virtual Bill Bill { get; set; }
    }
}
