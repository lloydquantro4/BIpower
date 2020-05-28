using Microsoft.EntityFrameworkCore;

namespace BIpower.Models
{
    public class biContext: DbContext
    {
        public biContext(DbContextOptions<biContext>options): base(options){}

        public DbSet<Customer> Customers{get;set;}
        public DbSet<Order> Orders{get;set;}
        public DbSet<Server> Servers{get;set;} 
    }
}