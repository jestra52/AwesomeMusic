namespace AwesomeMusic.Controllers
{
    using System.Threading.Tasks;
    using AwesomeMusic.Data.Commands.UserCommands;
    using AwesomeMusic.Data.DTOs;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUpAsync(CreateUserCommand request)
        {
            var response = await _mediator.Send(request);

            return response.Error is null ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateAsync(AuthenticateCommand request)
        {
            var response = await _mediator.Send(request);

            return response.Error is null ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
