namespace AwesomeMusic.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AwesomeMusic.Data.DTOs;
    using AwesomeMusic.Data.Queries.SongQueries;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class SongController : Controller
    {
        private readonly IMediator _mediator;

        public SongController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetSongs")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<SongDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSongsAsync([FromQuery] GetAllSongsQuery request)
        {
            var response = await _mediator.Send(request);

            return response.Error is null
                ? Ok(response)
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
