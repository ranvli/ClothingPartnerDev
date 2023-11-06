using ClothingPartnerAPI.DAL.Contracts;
using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Services.Contracts;

namespace ClothingPartnerAPI.Services
{
    public class EmployeeService : Service<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository Repository;

        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
            Repository = repository;
        }
    }
}
