using ClothingPartnerAPI.DAL.Context;
using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.Models;

namespace ClothingPartnerAPI.DAL
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        ClothingPartnerContext clothingPartnerContext = null;
        public DepartmentRepository(ClothingPartnerContext context): base(context)
        {
            clothingPartnerContext = context;
        }
    }
}
