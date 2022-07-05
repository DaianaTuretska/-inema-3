using MediatR;
using Microsoft.AspNetCore.Mvc;
using CinemaApplication.Commands.Reservations.DeleteReservation;
using CinemaApplication.Commands.Reservations.InsertReservation;
using CinemaApplication.Commands.Reservations.UpdateReservation;
using CinemaApplication.DTO.Requests;
using CinemaApplication.DTO.Responses;
using CinemaApplication.Exceptions;
using CinemaApplication.Queries.Services.GetAllServices;
using CinemaApplication.Queries.Services.GetByIdService;

namespace CinemaCleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator mediator;

        public ServiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReservationResponse>>> Get()
        {
            try
            {
                var results = await mediator.Send(new GetAllReviewsQuery());
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        // GET: api/ServiceDiscount/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReservationResponse>> Get(int id)
        {
            try
            {
                var result = await mediator.Send(new GetByIdReviewQuery { Id = id});
                return Ok(result);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] ReservationRequest request)
        {
            try
            {
                await mediator.Send(new InsertReservationCommand { ReviewRequest = request });
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] ReservationRequest request)
        {
            try
            {
                await mediator.Send(new UpdateReservationCommand { ReservationRequest = request});
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
        

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteReservationCommand { Id = id });
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }
    }
}
