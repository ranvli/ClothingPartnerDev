using ClothingPartnerAPI.DAL.Context;
using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.Models;

namespace ClothingPartnerAPI.DAL
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        ClothingPartnerContext clothingPartnerContext = null;
        public EmployeeRepository(ClothingPartnerContext context) : base(context)
        {
            clothingPartnerContext = context;
        }

        //additional methods here...
    }
}
