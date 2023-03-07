using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebBanVeMayBay.Models
{
    public class WebBanVeMayBayDBContext:DbContext
    {
        public WebBanVeMayBayDBContext():base("name=AirTicketSales") {}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Complain> Complain { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}