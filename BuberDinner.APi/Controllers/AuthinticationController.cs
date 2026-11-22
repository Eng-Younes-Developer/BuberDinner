using BuberDinner.Application.Authintication;
using BuberDinner.Contracts.Authintication;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Application.Authintication.ResponseAuth;
using MediatR;
using BuberDinner.Application.Authintication.Commands.Register;
using BuberDinner.Application.Authintication.Queries.Login;
namespace BuberDinner.APi.Controllers

{
    [ApiController]
    [Route("Authintication")]
    public class AuthinticationController : ControllerBase
    {
        private readonly ISender _sender ;

        public AuthinticationController(ISender sender)
        {

            _sender = sender;
        }
    
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Register request)
        {
            var command = new RegisterCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);
            var authinticationResult = await _sender.Send(command);

            var response = new AuthResponse
            {
                Id = authinticationResult.user.Id,
                FirstName = authinticationResult.user.FirstName,
                LastName = authinticationResult.user.LastName,
                Email = authinticationResult.user.Email,
                Token = authinticationResult.Token
            };
            return Ok(response);
        }

        [HttpPost]  
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
           var query = new LoginQuery(
                request.Email,
                request.Password);

           var authinticationResult = await _sender.Send(query);

            var response = new AuthResponse
            {
                Id = authinticationResult.user.Id,
                FirstName = authinticationResult.user.FirstName,
                LastName = authinticationResult.user.LastName,
                Email = authinticationResult.user.Email,
                Token = authinticationResult.Token
            };
            return Ok(response);
            }


    }
}
