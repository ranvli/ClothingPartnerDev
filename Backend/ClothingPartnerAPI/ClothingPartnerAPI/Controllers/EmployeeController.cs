using AutoMapper;
using ClothingPartnerAPI.DTO;
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
        private IDepartmentService _departmentService;
        private IDesignationService _designationService;
        private ITeamService _teamService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IDesignationService designationService, ITeamService teamservice, IMapper mapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _designationService = designationService;
            _teamService = teamservice;
            _mapper = mapper;
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
        [Route("employee-add")]
        public IActionResult AddEmployee([FromBody] EmployeeDTO employeeCreateDTO)
        {
            ResponseDto<Employee> response = new ResponseDto<Employee>();

            try
            {
                Employee newEmployee  =  _mapper.Map<Employee>(employeeCreateDTO);
                newEmployee.Department = _departmentService.Get(employeeCreateDTO.DepartmentId);
                newEmployee.Designation = _designationService.Get(employeeCreateDTO.DesignationId);
                newEmployee.Team = _teamService.Get(employeeCreateDTO.TeamId);
                
                
                var result = _employeeService.Add(newEmployee);
                response.ResultOkMessage = "Employee added successfully.";
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
        public IActionResult EmployeeUpdate(int employeeId, EmployeeDTO employeeUpdateDTO)
        {
            ResponseDto<Employee> response = new ResponseDto<Employee>();

            try
            {
                Employee employeeUpdate = _employeeService.Get(employeeId);
                if (employeeUpdate != null)
                {
                    _mapper.Map(employeeUpdateDTO, employeeUpdate);

                    employeeUpdate.Department = _departmentService.Get(employeeUpdateDTO.DepartmentId);
                    employeeUpdate.Designation = _designationService.Get(employeeUpdateDTO.DesignationId);
                    employeeUpdate.Team = _teamService.Get(employeeUpdateDTO.TeamId);

                    _employeeService.Update(employeeUpdate);
                    response.Data = employeeUpdate;
                    response.ResultOkMessage = "Ok";
                    return Ok(response);
                }
                else {
                    response.Error.Message = "Employee not found.";
                    response.Error.Code = 404; // Código de recurso no encontrado
                    return NotFound(response);
                }
                
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
