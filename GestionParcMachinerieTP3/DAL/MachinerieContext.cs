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

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<BillCommand> BillCommands { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
