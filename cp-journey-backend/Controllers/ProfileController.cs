using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

public class ProfileController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}