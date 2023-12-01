using Microsoft.AspNetCore.Identity;

namespace ClothingPartnerAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        //associated employee
        public int EmployeeId { get; set; }
    }
}
