using System;
using System.Collections.Generic;

namespace GestionParcMachinerieTP3.Models
{
    public partial class BillCommands
    {
        public int BillCommandsID { get; set; }
        public int CommandID { get; set; }
        public int BillID { get; set; }

        public virtual Command Command { get; set; }
        public virtual Bill Bill { get; set; }
    }
}
