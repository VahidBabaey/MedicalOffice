using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthCheckController : Controller
{
    [HttpGet]
    public IActionResult Ping()
    {
        return Content($"Pung from SelakTeb MedicalOffice application on {DateTime.Now}");
    }
}
