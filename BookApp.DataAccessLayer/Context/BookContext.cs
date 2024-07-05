using BookApp.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DataAccessLayer.Context
{
    public class BookContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=AK;initial catalog=BookAppDB;integrated security=true");
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookNote> BookNotes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<SharedNote> SharedNotes { get; set; }
        public DbSet<ShelfLocation> ShelfLocations { get; set; }


    }
}
