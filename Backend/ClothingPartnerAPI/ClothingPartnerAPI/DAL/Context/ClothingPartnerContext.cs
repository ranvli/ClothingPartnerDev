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
                optionsBuilder.UseSqlServer("Server=RANVLI-ACER;Database=ClothingPartnerDB;User=sa;Password=Pa$$w0rd;Encrypt=false");
            }
        }
    }
}
