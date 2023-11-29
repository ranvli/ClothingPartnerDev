using ClothingPartnerAPI.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public int DesignationId { get; set; }
        public virtual Designation Designation { get; set; }

        [JsonIgnore]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [JsonIgnore]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public string EmergencyPhone { get; set; }
        public string RecentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string VoterId { get; set; }
        public string PassportNumber { get; set; }
        public string DrivingLicense { get; set; }
        public string SalaryBankAccount { get; set; }

        public string WeChatId { get; set; }
        public string WhatsappNumber { get; set; }
        public string LastCompanyNOC { get; set; }

    }
}
