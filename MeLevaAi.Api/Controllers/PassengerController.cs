using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Contracts.Requests.Passenger;
using MeLevaAi.Api.Contracts.Requests.Ride;
using MeLevaAi.Api.Contracts.Responses.Passenger;
using MeLevaAi.Api.Contracts.Responses.Ride;
using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Repositories;
using MeLevaAi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("passengers")]
    public class PassengerController : Controller
    {
        private readonly PassengerService _passengerService;
        private readonly RideRepository _rideRepository;

        public PassengerController()
        {
            _passengerService = new PassengerService();
            _rideRepository = new RideRepository();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassengerResponse))]
        public ActionResult<IEnumerable<Passenger>> List()
        {
            var response = _passengerService.List();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Passenger?> Get([FromRoute] Guid id)
        {
            var response = _passengerService.Get(id);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PassengerResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<PassengerResponse> Register([FromBody] RegisterPassengerRequest request)
        {
            var response = _passengerService.Register(request);

            if(!response.IsValid())
            {
                return BadRequest(new ErrorResponse(response.Notifications));
            }

            return CreatedAtAction("Get", new { id = response.Passenger.Id }, response);
        }

        [HttpPut("{id:guid}/add-money")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassengerResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<AddedMoneyResponse> AddMoney([FromBody] AddMoneyRequest request, [FromRoute] Guid id)
        {
            var response = _passengerService.AddMoney(request, id);

            if(!response.IsValid())
            {
                return NotFound(new ErrorResponse(response.Notifications));
            }

            return Ok(response);
        }
        
        [HttpPost("request-ride")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RideResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<RideResponse> RequestRide([FromBody] RequestRideRequest request)
        {
            var response = _passengerService.RequestRide(request);

            if(!response.IsValid())
            {
                return BadRequest(new ErrorResponse(response.Notifications));
            }

            return CreatedAtAction("GetRide", new { id = response.Ride.Id }, response);
        }

        [HttpGet("{id:int}/get-ride")]
        public ActionResult<Ride?> GetRide([FromRoute] int id)
        {
            var response = _rideRepository.Get(id);
            return Ok(response);
        }
    }
}
