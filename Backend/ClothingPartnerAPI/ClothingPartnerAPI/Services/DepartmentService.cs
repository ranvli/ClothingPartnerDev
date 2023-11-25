using ClothingPartnerAPI.DAL;
using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Services.Contracts;

namespace ClothingPartnerAPI.Services
{
    public class DepartmentService : Service<Department>, IDepartmentService
    {
        private readonly IDepartmentRepository Repository;

        public DepartmentService(IDepartmentRepository repository) : base(repository)
        {
            Repository = repository;
        }
    }
}
