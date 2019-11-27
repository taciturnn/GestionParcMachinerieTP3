using System;
using System.Collections.Generic;

namespace GestionParcMachinerieTP3.Models
{
    public partial class Command
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int? MachineId { get; set; }
        public long? From { get; set; }
        public long? To { get; set; }
        public string Status { get; set; }

        public virtual Client Client { get; set; }
        public virtual Machine Machine { get; set; }
    }
}
