using Microsoft.EntityFrameworkCore;
using ClothingPartnerAPI.Models;

namespace ClothingPartnerAPI.DAL.Context
{
    public class ClothingPartnerContext : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=RANVLI-ACER;Database=ClothingPartnerDB;User=sa;Password=Pa$$w0rd;Encrypt=false");
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ClothingPartnerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
    }
}
