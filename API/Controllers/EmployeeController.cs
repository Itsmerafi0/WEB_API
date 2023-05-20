using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeeController : ControllerBase
{
    private readonly IController<Employee> _employeeRepository;
    public EmployeeController(IController<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public IActionResult Getall()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return NotFound();
        }

        return Ok(employees);
    }

    [HttpGet("{guid}")]
    public IActionResult GetbyGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        { return NotFound(); }
        return Ok(employee);
    }

    [HttpPost]
    public IActionResult Create(Employee employee)
    {
        var result = _employeeRepository.Create(employee);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Put(Employee employee)
    {
        var isUpdated = _employeeRepository.Update(employee);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();

    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _employeeRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
