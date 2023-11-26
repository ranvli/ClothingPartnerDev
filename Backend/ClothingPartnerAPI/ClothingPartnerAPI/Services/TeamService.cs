using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Services.Contracts;

namespace ClothingPartnerAPI.Services
{
    public class TeamService : Service<Team>, ITeamService
    {
        private readonly ITeamRepository Repository;
        public TeamService(ITeamRepository repository) : base(repository)
        {
            Repository = repository;
        }
    }
}
