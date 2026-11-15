using BuberDinner.Application.services.Authintication;
using BuberDinner.Contracts.Authintication;
using Microsoft.AspNetCore.Mvc;
namespace BuberDinner.APi.Controllers
{
    [ApiController]
    [Route("Authintication")]
    public class AuthinticationController : ControllerBase
    {
        private readonly IAuthinticationService _authinticationService;

        public AuthinticationController(IAuthinticationService authinticationService)
        {
            _authinticationService = authinticationService;
        }
    
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(Register request)
        {
            var authinticationResult = _authinticationService.Register(
                request.FirstName,
                request.LastName, 
                request.Email, 
                request.Password);

            var response = new AuthResponse
            {
                Id = authinticationResult.Id,
                FirstName = authinticationResult.FirstName,
                LastName = authinticationResult.LastName,
                Email = authinticationResult.Email,
                Token = authinticationResult.Token
            };
            return Ok(response);
        }

        [HttpPost]  
        [Route("Login")]
        public IActionResult Login(LoginRequest request)
        {
            var authinticationResult = _authinticationService.Login(
                request.Email, 
                request.Password);

            var response = new AuthResponse
            {
                Id = authinticationResult.Id,
                FirstName = authinticationResult.FirstName,
                LastName = authinticationResult.LastName,
                Email = authinticationResult.Email,
                Token = authinticationResult.Token
            };
            return Ok(authinticationResult);
        }


    }
}
