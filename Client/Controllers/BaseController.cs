using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers;

public class BaseController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
