using ClothingPartnerAPI.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClothingPartnerAPI.Models
{

 public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string? PersonalEmail { get; set; }
        public string CompanyEmail { get; set; }

        public string RecentPicture { get; set; }
        public string Phone { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Team Team { get; set; }
        //public Employee Supervisor { get; set; }

        public string EmergencyPhone { get; set; }
        public string RecentAddress { get; set; }
        public string PermanetAddress { get; set; }
        public BloodGroup MyProperty { get; set; }
        public DateTime DataOfBirth { get; set; }
        public DateTime DataOfJoining { get; set; }
        public string VoterId { get; set; }
        public string PassportNumber { get; set; }
        public string DrivingLicense { get; set; }
        public string SalaryBankAccount { get; set; }

        public string WeChatId { get; set; }
        public string WhatsappNumber { get; set; }
        public string LastCompanyNOC { get; set; }


    }
}
