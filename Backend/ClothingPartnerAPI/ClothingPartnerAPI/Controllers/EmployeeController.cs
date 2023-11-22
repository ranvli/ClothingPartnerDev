using AutoMapper;
using ClothingPartnerAPI.DTO.Base;
using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Services;
using ClothingPartnerAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ClothingPartnerAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("employee-get-by-id")]
        public IActionResult EmployeeGetById(int id)
        {
            ResponseDto<Employee> response = new ResponseDto<Employee>();

            try
            {
                var employee = _employeeService.Get(id);
                response.Data = employee;
                response.ResultOkMessage = "Ok";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Error.ExceptionMessage = e.Message;
                response.Error.Message = "Internal Error";
                response.Error.Code = 1;
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("employee-get-all")]
        public IActionResult EmployeeGetAll()
        { 
            ResponseDto<List<Employee>> response = new ResponseDto<List<Employee>>();
            try
            {
                var employees = _employeeService.GetAll();
                response.Data = employees.ToList();
                response.ResultOkMessage = "Ok";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Error.ExceptionMessage = e.Message;
                response.Error.Message = "Internal Error";
                response.Error.Code = 1;
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("employee-post")]
        public IActionResult AddEmployee(Employee employee)
        {
            ResponseDto<List<Employee>> response = new ResponseDto<List<Employee>>();
            Console.WriteLine(employee);
            try
            {
                var result = _employeeService.Add(employee);
                response.Data = null;
                response.ResultOkMessage = "Ok";
                return Ok(result);
            }
            catch (Exception e)
            {
                response.Error.ExceptionMessage = e.Message;
                response.Error.Message = "Internal Error";
                response.Error.Code = 1;
                return BadRequest(response);
            }
        }

        [HttpDelete]
        [Route("employee-delete")]
        public IActionResult EmployeeDelete(int employeeId)
        {
            ResponseDto<List<Employee>> response = new ResponseDto<List<Employee>>();
            
            try
            {
                var result = _employeeService.Delete(employeeId);
                response.ResultOkMessage = "Ok";
                return Ok(result);
            }
            catch (Exception e)
            {
                response.Error.ExceptionMessage = e.Message;
                response.Error.Message = "Internal Error";
                response.Error.Code = 1;
                return BadRequest(response);
            }
        }

        [HttpPut]
        [Route("employee-update")]
        public IActionResult EmployeeUpdate(int employeeId, Employee employee)
        {
            ResponseDto<List<Employee>> response = new ResponseDto<List<Employee>>();

            try
            {
                _employeeService.Update(employee);
                response.ResultOkMessage = "Ok";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Error.ExceptionMessage = e.Message;
                response.Error.Message = "Internal Error";
                response.Error.Code = 1;
                return BadRequest(response);
            }
        }
    }
}
