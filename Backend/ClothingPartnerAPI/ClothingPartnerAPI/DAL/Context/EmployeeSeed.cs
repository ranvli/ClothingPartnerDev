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
                UserName = "ranvli",
                Email = "ranvli@ranvli.com",
            };

            // Add initial user to db
            var existingUser = _userManager.FindByNameAsync(initialUser.UserName).Result;
            if (existingUser == null)
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
        }
    }
}
