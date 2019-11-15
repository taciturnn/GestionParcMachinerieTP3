using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestionParcMachinerieTP3.Models.Db
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Command> Command { get; set; }
        public virtual DbSet<Machine> Machine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bill");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.Commands)
                    .HasColumnName("commands")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Paid).HasColumnName("paid");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Bill)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("bill_client_id_fk");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Command>(entity =>
            {
                entity.ToTable("command");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.From).HasColumnName("from");

                entity.Property(e => e.MachineId).HasColumnName("machine_id");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.To).HasColumnName("to");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Command)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("command_client_id_fk");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Command)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("command_machine_id_fk");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.ToTable("machine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RentPrice).HasColumnName("rentPrice");
            });
        }
    }
}
