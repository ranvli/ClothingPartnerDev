using Microsoft.EntityFrameworkCore;
using ClothingPartnerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace ClothingPartnerAPI.DAL.Context
{
    public class ClothingPartnerContext : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<Team> Team { get; set; }

        public ClothingPartnerContext(DbContextOptions<ClothingPartnerContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies(); // If you want to enable lazy loading (optional)
                optionsBuilder.EnableSensitiveDataLogging(); // If you want to log sensitive data (optional)
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
