using System.ComponentModel.DataAnnotations;

namespace ClothingPartnerAPI.Models
{
    public class Designation
    {
        [Key]
        public int DesignationId { get; set; }
        public string Description { get; set; }

    }
}
