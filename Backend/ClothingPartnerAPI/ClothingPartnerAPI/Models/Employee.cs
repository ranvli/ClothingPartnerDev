using System.ComponentModel.DataAnnotations;

namespace ClothingPartnerAPI.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
    }
}
