using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace BuberDinner.APi.Controllers
{
    [Route("api/Dinner")]
    [ApiController]
    [Authorize]
    public class DinnerController : ControllerBase
    {
        [HttpGet]
        public IActionResult listDinners()
        {
            return Ok(Array.Empty<String>());
        }

    }
}
