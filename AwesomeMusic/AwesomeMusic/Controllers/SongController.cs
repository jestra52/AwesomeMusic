namespace AwesomeMusic.Controllers
{
    using System.Threading.Tasks;
    using AwesomeMusic.Data.Commands.SongCommands;
    using AwesomeMusic.Data.DTOs;
    using AwesomeMusic.Data.Queries.SongQueries;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// SongController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SongController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="mediator"></param>
        public SongController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all public and private songs, with the option of excluding public Songs
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>A collection of Songs</returns>
        /// <response code="200">Returns a collection of Songs</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If an error occurs</response>
        [HttpGet("GetSongs")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<PageableResult<SongDto>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<PageableResult<SongDto>>))]
        public async Task<IActionResult> GetSongsAsync([FromQuery] GetAllSongsQuery request)
        {
            var response = await _mediator.Send(request);

            return response.Error is null
                ? Ok(response)
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        /// <summary>
        /// Create a Song
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>The created Song</returns>
        /// <response code="200">Returns the created Song</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">If an error occurs</response>
        [HttpPost("CreateSong")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<SongDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<SongDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<SongDto>))]
        public async Task<IActionResult> CreateSongAsync(CreateSongCommand request)
        {
            var response = await _mediator.Send(request);

            return response.Error is null
                ? Ok(response)
                : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        /// <summary>
        /// Updates a Song
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>The updated Song</returns>
        /// <response code="200">Returns the updated Song</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">No songs found</response>
        /// <response code="500">If an error occurs</response>
        [HttpPut("UpdateSong")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<SongDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<SongDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<SongDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<SongDto>))]
        public async Task<IActionResult> UpdateSongAsync(UpdateSongCommand request)
        {
            var response = await _mediator.Send(request);
            var errorActionResult = HttpContext.Response.StatusCode != StatusCodes.Status500InternalServerError
                ? StatusCode(HttpContext.Response.StatusCode, response)
                : StatusCode(StatusCodes.Status500InternalServerError, response);

            return response.Error is null
                ? Ok(response)
                : errorActionResult;
        }

        /// <summary>
        /// Deletes a Song
        /// </summary>
        /// <param name="request">Request</param>
        /// <response code="204">No content</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">No songs found</response>
        /// <response code="500">If an error occurs</response>
        [HttpDelete("DeleteSong")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<bool>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<bool>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<bool>))]
        public async Task<IActionResult> DeleteSongAsync(DeleteSongCommand request)
        {
            var response = await _mediator.Send(request);
            var errorActionResult = HttpContext.Response.StatusCode != StatusCodes.Status500InternalServerError
                ? StatusCode(HttpContext.Response.StatusCode, response)
                : StatusCode(StatusCodes.Status500InternalServerError, response);

            return response.Error is null
                ? NoContent()
                : errorActionResult;
        }
    }
}
