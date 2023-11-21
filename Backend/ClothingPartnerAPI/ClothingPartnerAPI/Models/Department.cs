using System.ComponentModel.DataAnnotations;

namespace ClothingPartnerAPI.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Description { get; set; }
    }
}
