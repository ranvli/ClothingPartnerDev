using ClothingPartnerAPI.DAL.Context;
using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.Models;

namespace ClothingPartnerAPI.DAL
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        ClothingPartnerContext clothingPartnerContext = null;
        public TeamRepository(ClothingPartnerContext context) : base(context)
        {
            clothingPartnerContext = context;
        }
    }
}
