using API.Contracts;
using API.Models;
using API.Utility;
using API.ViewModels.Employees;
using API.ViewModels.Others;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper<Employee, EmployeeVM> _mapper;

    public EmployeeController(IEmployeeRepository employeeRepository, IMapper<Employee, EmployeeVM> mapper )
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    [HttpGet("GetAllMasterEmployee")]
    public IActionResult GetAll()
    {
        var masterEmployees = _employeeRepository.GetAllMasterEmployee();
        if (!masterEmployees.Any())
        {
            return NotFound(new ResponseVM<List<MasterEmployeeVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }

        return Ok(new ResponseVM<IEnumerable<MasterEmployeeVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data ditampilkan",
            Data = masterEmployees
           
        });
    }

    [HttpGet("GetMasterEmployeeByGuid")]
    public IActionResult GetMasterEmployeeByGuid(Guid guid)
    {
        var masterEmployees = _employeeRepository.GetMasterEmployeeByGuid(guid);
        if (masterEmployees is null)
        {
            return NotFound(new ResponseVM<MasterEmployeeVM> {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message ="Not Found"
            });
        }

        return Ok(new ResponseVM<MasterEmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found",
            Data = masterEmployees
        });
    }

    [HttpGet]
    public IActionResult Getall()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return NotFound(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        var resultConverted = employees.Select(_mapper.Map).ToList();
        return Ok(new ResponseVM<List<EmployeeVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found",
            Data = resultConverted
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetbyGuid(Guid guid)
    {

        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        { 
            return NotFound(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "NotFound"
            }); 
        }
        var data = _mapper.Map(employee);
        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found",
            Data = data
        });
    }

    [HttpPost]
    public IActionResult Create(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var result = _employeeRepository.Create(employeeConverted);
        if (result is null)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed Create Employee"
            });
        }

        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Create Employee"
        });
    }

    [HttpPut]
    public IActionResult Put(EmployeeVM employeeVM)
    {
        var employeeConverted = _mapper.Map(employeeVM);
        var isUpdated = _employeeRepository.Update(employeeConverted);
        if (!isUpdated)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed Update Employee"
            });
        }

        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Update Employee" 
        });

    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _employeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Failed Delete Employee"
            });
        }

        return Ok(new ResponseVM<EmployeeVM>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success Delete Employee"
        });
    }
}
