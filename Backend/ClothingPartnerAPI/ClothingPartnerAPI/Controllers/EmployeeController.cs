using AutoMapper;
using ClothingPartnerAPI.DTO;
using ClothingPartnerAPI.DTO.Base;
using ClothingPartnerAPI.Models;
using ClothingPartnerAPI.Services;
using ClothingPartnerAPI.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public EmployeeController(UserManager<ApplicationUser> userManager, IEmployeeService employeeService, IDepartmentService departmentService, IDesignationService designationService, ITeamService teamservice, IMapper mapper)
        {
            _userManager = userManager;
            _employeeService = employeeService;
            _departmentService = departmentService;
            _designationService = designationService;
            _teamService = teamservice;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("employee-get-by-id")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult EmployeeGetById(int id)
        {
            ResponseDTO<Employee> response = new ResponseDTO<Employee>();

            try
            {
                Employee employee = _employeeService.Get(id);
                response.Data = employee;
                response.Message = "Ok";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Error.ExceptionMessage = e.Message;
                
                if (e.Message == "ErrNotFound") {
                    response.Error.Message = "Not Found";
                    response.Error.Code = 404;
                    return NotFound(response);
                }   
                else
                {
                    response.Error.Message = "Internal Error";
                    response.Error.Code = 400;
                    return BadRequest(response);
                }
            }
        }

        [HttpGet]
        [Route("employee-get-all")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult EmployeeGetAll()
        { 
            ResponseDTO<List<Employee>> response = new ResponseDTO<List<Employee>>();

            try
            {
                var employees = _employeeService.GetAll();
                response.Data = employees.ToList();
                response.Message = "Ok";
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult AddEmployee([FromBody] EmployeeDTO employeeCreateDTO)
        {
            ResponseDTO<Employee> response = new ResponseDTO<Employee>();

            try
            {
                Employee newEmployee  =  _mapper.Map<Employee>(employeeCreateDTO);
                newEmployee.Department = _departmentService.Get(employeeCreateDTO.DepartmentId);
                newEmployee.Designation = _designationService.Get(employeeCreateDTO.DesignationId);
                newEmployee.Team = _teamService.Get(employeeCreateDTO.TeamId);
                
                var result = _employeeService.Add(newEmployee);
                response.Data = newEmployee;
                response.Message = "Employee added successfully.";

                //add ApplicationUser and associate to the new created employee
                var user = new ApplicationUser()
                {
                    UserName = employeeCreateDTO.CompanyEmail.Split('@')[0].ToString(),
                    Email = employeeCreateDTO.CompanyEmail,
                    EmployeeId = newEmployee.EmployeeId
                };
                var userResult = _userManager.CreateAsync(user, employeeCreateDTO.Password).Result;
                if (userResult.Succeeded)
                {
                    // User created successfully
                    return Ok(response);
                }
                else
                {
                    //rollback employee creation
                    _employeeService.Delete(newEmployee.EmployeeId);
                    
                    response.Error.Message = "Error creating user.";
                    response.Error.Code = 500;
                    return StatusCode(500, response);
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

        [HttpDelete]
        [Route("employee-delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult EmployeeDelete(int employeeId)
        {
            ResponseDTO<Employee> response = new ResponseDTO<Employee>();
            
            try
            {
                var result = _employeeService.Delete(employeeId);
                if (result)
                {
                    response.Message = "Ok";
                    return Ok(response);
                }
                else {
                    response.Error.Message = "Internat error, not found";
                    response.Error.Code = 404;
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

        [HttpPut]
        [Route("employee-update")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult EmployeeUpdate(int employeeId, EmployeeDTO employeeUpdateDTO)
        {
            ResponseDTO<Employee> response = new ResponseDTO<Employee>();

            try
            {
                Employee employeeUpdate = _employeeService.Get(employeeId);
                if (employeeUpdate != null)
                {
                    _mapper.Map(employeeUpdateDTO, employeeUpdate);
                    employeeUpdate.EmployeeId = employeeId;
                    employeeUpdate.Department = _departmentService.Get(employeeUpdateDTO.DepartmentId);
                    employeeUpdate.Designation = _designationService.Get(employeeUpdateDTO.DesignationId);
                    employeeUpdate.Team = _teamService.Get(employeeUpdateDTO.TeamId);

                    _employeeService.Update(employeeUpdate);
                    response.Data = employeeUpdate;
                    response.Message = "Ok";
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
