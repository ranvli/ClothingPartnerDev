using ClothingPartnerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingPartnerAPI.DAL.Context
{
    public class EmployeeSeed : IEntityTypeConfiguration<Employee>
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeeSeed(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            var initialUser = new Employee
            {
                UserName = "admin",
                Email = "admin@admin.com",
            };

            // Add initial user to db
            bool initialSeed = false;
            var existingUser = _userManager.FindByNameAsync(initialUser.UserName).Result;
            
            if (initialSeed && existingUser == null)
            {
                try
                {
                    var result = _userManager.CreateAsync(initialUser, "Pa$$w0rd").Result;
                    if (result.Succeeded)
                    {
                        // User created successfully
                    }
                    else
                    {
                        throw new Exception("Error creating initial user.");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
