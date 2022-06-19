using Microsoft.AspNetCore.Mvc;

namespace ARS.Modules.IOT.Controllers;

[ApiController]
[Route("IOT/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet(Name = "Test")]
    public bool Get()
    {
        return true;
    }
}