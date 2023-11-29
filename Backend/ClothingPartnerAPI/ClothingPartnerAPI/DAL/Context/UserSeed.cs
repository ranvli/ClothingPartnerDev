using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClothingPartnerAPI.DAL.Context
{
    public class UserSeed : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSeed(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var initialEmployee = new Employee
            {
                //UserName = "admin",
                //Email = "admin@admin.com",
                DepartmentId = 1,
                TeamId = 1,
                DesignationId = 1,
                CompanyEmail = "admin@admin.com",
                FullName = "Admin",
                DateOfBirth = DateTime.Now,
                DateOfJoining = DateTime.Now,
                RecentAddress = "Dhaka",
                DrivingLicense = "123456789",
                EmergencyPhone = "123456789",
                LastCompanyNOC = "123456789",
                PassportNumber = "123456789",
                PermanentAddress = "Dhaka",
                Phone = "123456789",
                RecentPicture = "123456789",
                SalaryBankAccount = "123456789",
                VoterId = "123456789",
                WeChatId = "123456789",
                WhatsappNumber = "123456789",
                BloodGroup = BloodGroup.APositive
            };

            var initialUser = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            // Add initial user to db
            bool initialSeed = true;
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
