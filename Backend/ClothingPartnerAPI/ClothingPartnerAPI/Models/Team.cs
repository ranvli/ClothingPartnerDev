using System.ComponentModel.DataAnnotations;

namespace ClothingPartnerAPI.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string TeamDesciption { get; set; }
    }
}
