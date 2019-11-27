using GestionParcMachinerieTP3.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GestionParcMachinerieTP3.DAL
{
    public partial class MachinerieContext : DbContext
    {
        public MachinerieContext() : base("MachinerieContext")
        {
        }

        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Command> Command { get; set; }
        public virtual DbSet<Machine> Machine { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
