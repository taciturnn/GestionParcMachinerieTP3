﻿using System;
using System.Collections.Generic;

namespace GestionParcMachinerieTP3.Models.Db
{
    public partial class Machine
    {
        public Machine()
        {
            Command = new HashSet<Command>();
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public int RentPrice { get; set; }

        public virtual ICollection<Command> Command { get; set; }
    }
}
