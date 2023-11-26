using ClothingPartnerAPI.DAL.Context;
using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Services.Contracts;

namespace ClothingPartnerAPI.DAL
{
    public class DesignationRepository : Repository<Designation>, IDesignationRepository
    {
        ClothingPartnerContext clothingPartnerContext = null;
        public DesignationRepository(ClothingPartnerContext context) : base(context)
        {
            clothingPartnerContext = context;
        }
    }
}
