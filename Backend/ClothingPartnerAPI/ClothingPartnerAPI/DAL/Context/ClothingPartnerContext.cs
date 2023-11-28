﻿using Microsoft.EntityFrameworkCore;
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

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:clothingpartnerdevdb.database.windows.net,1433;Initial Catalog=ClothingPartnerDevDB;Persist Security Info=False;User ID=ranvlicr;Password=Pa$$w0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                //optionsBuilder.UseSqlServer("Server=RANVLI-ACER;Database=ClothingPartnerDB;User=sa;Password=Pa$$w0rd;Encrypt=false");
                //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ClothingPartnerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

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
