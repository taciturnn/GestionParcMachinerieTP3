﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GestionParcMachinerieTP3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<GestionParcMachinerieTP3.Models.AccountViewModel> AccountViewModels { get; set; }

        public System.Data.Entity.DbSet<GestionParcMachinerieTP3.Models.Machine> Machines { get; set; }

        public System.Data.Entity.DbSet<GestionParcMachinerieTP3.Models.CartItemViewModel> CartItemViewModels { get; set; }

        public System.Data.Entity.DbSet<GestionParcMachinerieTP3.Models.Command> Commands { get; set; }

        public System.Data.Entity.DbSet<GestionParcMachinerieTP3.Models.CommandViewModel> CommandViewModels { get; set; }

        public System.Data.Entity.DbSet<GestionParcMachinerieTP3.Models.Bill> Bills { get; set; }

        public System.Data.Entity.DbSet<GestionParcMachinerieTP3.Models.AspNetUser> AspNetUsers { get; set; }
    }
}