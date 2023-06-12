using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Contracts.Requests.Driver;
using MeLevaAi.Api.Contracts.Requests.Passenger;
using MeLevaAi.Api.Contracts.Responses.Driver;
using MeLevaAi.Api.Contracts.Responses.Passenger;
using MeLevaAi.Api.Contracts.Responses.Ride;
using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : Controller
    {
        private readonly DriverService _driverService;

        public DriverController()
        {
            _driverService = new DriverService();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverResponse))]
        public ActionResult<IEnumerable<Driver>> List()
        {
            var drivers = _driverService.List();
            return Ok(drivers);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Driver?> Get([FromRoute] Guid id)
        {
            var driver = _driverService.Get(id);
            return Ok(driver);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DriverResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<Driver> Register([FromBody] RegisterDriverRequest driver)
        {
            var newDriver = _driverService.Register(driver);

            if (!newDriver.IsValid())
                return BadRequest(new ErrorResponse(newDriver.Notifications));

            return CreatedAtAction("Get", new { id = newDriver.Driver.Id }, newDriver);
        }
        
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<DriverResponse> Remove([FromRoute] Guid id)
        {
            var response = _driverService.Remove(id);

            if (!response.IsValid())
                return BadRequest(new ErrorResponse(response.Notifications));

            return Ok(response);
        }

        [HttpPut("{id:guid}/withdraw-money")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<AddedMoneyResponse> AddMoney([FromBody] WithdrawMoneyRequest request, [FromRoute] Guid id)
        {
            var response = _driverService.WithdrawMoney(request, id);

            if (!response.IsValid())
            {
                return NotFound(new ErrorResponse(response.Notifications));
            }

            return Ok(response);
        }

        [HttpPut("{id:int}/accept-ride")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StartRideResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<StartRideResponse> StartRide([FromRoute] int id)
        {
            var response = _driverService.StartRide(id);

            if (!response.IsValid())
            {
                return NotFound(new ErrorResponse(response.Notifications));
            }

            return Ok(response);
        }

        [HttpPut("{id:int}/finish-ride")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FinishRideResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public ActionResult<FinishRideResponse> FinishRide([FromRoute] int id)
        {
            var response = _driverService.FinishRide(id);

            if (!response.IsValid())
            {
                return NotFound(new ErrorResponse(response.Notifications));
            }

            return Ok(response);
        }
    }
}
