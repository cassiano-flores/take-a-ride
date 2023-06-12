using MeLevaAi.Api.Contracts.Requests.Passenger;
using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Contracts.Responses.Passenger;
using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Services;
using Microsoft.AspNetCore.Mvc;
using MeLevaAi.Api.Contracts.Responses.Vehicle;
using MeLevaAi.Api.Contracts.Requests.Vehicle;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("vehicles")]
    public class VehicleController : Controller
    {
        private readonly VehicleService _vehicleService;

        public VehicleController()
        {
            _vehicleService = new VehicleService(); 
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PassengerResponse))]
        public ActionResult<IEnumerable<Vehicle>> List()
        {
            var response = _vehicleService.List();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Vehicle?> Get([FromRoute] Guid id)
        {
            var response = _vehicleService.Get(id);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VehicleResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<VehicleResponse> Register([FromBody] RegisterVehicleRequest request)
        {
            var response = _vehicleService.Register(request);

            if (!response.IsValid())
            {
                return BadRequest(new ErrorResponse(response.Notifications));
            }

            return CreatedAtAction("Get", new { id = response.Vehicle.Id }, response);
        }
    }
}
