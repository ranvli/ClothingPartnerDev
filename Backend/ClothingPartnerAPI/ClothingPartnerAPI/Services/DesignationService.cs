using ClothingPartnerAPI.DAL;
using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Services.Contracts;

namespace ClothingPartnerAPI.Services
{
    public class DesignationService: Service<Designation>, IDesignationService
    {
        private readonly IDesignationRepository Repository;
        public DesignationService(IDesignationRepository repository): base(repository) {
            Repository = repository;
        }
    }
   
}
