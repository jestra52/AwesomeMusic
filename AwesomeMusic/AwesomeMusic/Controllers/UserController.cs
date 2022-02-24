namespace AwesomeMusic.Controllers
{
    using System.Threading.Tasks;
    using AwesomeMusic.Data.Commands.UserCommands;
    using AwesomeMusic.Data.DTOs;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// UserController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="mediator"></param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Service to register a new User
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>The created User</returns>
        /// <response code="200">Returns the created User</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">If an error occurs</response>
        [HttpPost("SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<UserDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<UserDto>))]
        public async Task<IActionResult> SignUpAsync(CreateUserCommand request)
        {
            var response = await _mediator.Send(request);

            return response.Error is null ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        /// <summary>
        /// Service to authenticate User with Email and Password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>The Bearer token to authorize requests</returns>
        /// <response code="200">Returns the Bearer token to authorize requests</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">If an error occurs</response>
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AuthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<AuthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<AuthResponseDto>))]
        public async Task<IActionResult> AuthenticateAsync(AuthenticateCommand request)
        {
            var response = await _mediator.Send(request);

            return response.Error is null ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
