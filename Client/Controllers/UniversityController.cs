using API.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Client.Repositories.Data;

namespace Client.Controllers;

//[Authorize]
public class UniversityController : Controller
{
    private readonly IUniversityRepository repository;

    public UniversityController(IUniversityRepository repository)
    {
        this.repository = repository;
    }
    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        var universities = new List<University>();

        if (result.Data != null)
        {
            universities = result.Data.Select(e => new University
            {
                Guid = e.Guid,
                Code = e.Code,
                Name = e.Name,

            }).ToList();
        }
        return View(universities);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await repository.Get(id);
        var university = new University();
        if (result.Data?.Guid is null)
        {
            return View(university);
        }
        else
        {
            university.Guid = result.Data.Guid;
            university.Name = result.Data.Name;
        }
        return View(university);
    }

 
}


