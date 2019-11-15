using System;
using System.Collections.Generic;

namespace GestionParcMachinerieTP3.Models.Db
{
    public partial class Client
    {
        public Client()
        {
            Bill = new HashSet<Bill>();
            Command = new HashSet<Command>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Bill> Bill { get; set; }
        public virtual ICollection<Command> Command { get; set; }
    }
}
