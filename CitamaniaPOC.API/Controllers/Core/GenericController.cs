using System.Net;

//using Microsoft.AspNetCore.Authorization;

namespace CitamaniaPOC.API.Controllers.Core
{
    //[Authorize]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class GenericController<TService, TController, TResponse, TPayload> : ControllerBase
    where TService : IGenericService<TResponse, TPayload>
    where TController : ControllerBase
    {
        protected readonly ILogger<TController> _logger;
        protected readonly TService _service;
        //protected string UserName => (User.Identity is { IsAuthenticated: true } ? User.Identity.Name : "")!;

        public GenericController(ILogger<TController> logger, TService service)
        {
            _logger = logger;
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [Produces("application/json")]
        [HttpGet]
        public virtual async Task<IActionResult> Get(bool? active)
        {
            var response = await _service.GetAll(active);
            if (response == null || !response.Any())
            {
                return NoContent();
            }
            else
            {
                return Ok(response);
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById([FromRoute] long id)
        {
            var response = await _service.GetByPk(id);

            if (response == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(response);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [Produces("application/json")]
        [HttpPost]
        public virtual async Task<IActionResult> Post(TPayload payload)
        {
            await _service.Create(payload, "TempUser");
            return StatusCode((int)HttpStatusCode.Created);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [Produces("application/json")]
        [HttpPut]
        public virtual async Task<IActionResult> Put(long id, TPayload payload)
        {
            await _service.Update(payload, "TempUser", id);
            return Ok();
        }
    }
}