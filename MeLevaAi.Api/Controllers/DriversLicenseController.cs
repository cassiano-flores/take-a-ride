using MeLevaAi.Api.Contracts;
using MeLevaAi.Api.Contracts.Requests.Driver;
using MeLevaAi.Api.Contracts.Requests.DriversLicense;
using MeLevaAi.Api.Contracts.Responses.Driver;
using MeLevaAi.Api.Contracts.Responses.DriversLicense;
using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeLevaAi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversLicenseController : Controller
    {
        private readonly DriversLicenseService _driversLicenseService;

        public DriversLicenseController()
        {
            _driversLicenseService = new DriversLicenseService();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriversLicenseResponse))]
        public ActionResult<IEnumerable<DriversLicense>> List()
        {
            var driversLicenses = _driversLicenseService.List();
            return Ok(driversLicenses);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<DriversLicense?> Get([FromRoute] Guid id)
        {
            var driversLicense = _driversLicenseService.Get(id);
            return Ok(driversLicense);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DriversLicenseResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public ActionResult<DriversLicense> Register([FromBody] RegisterDriversLicenseRequest driversLicense)
        {
            var newDriversLicense = _driversLicenseService.Register(driversLicense);

            if (!newDriversLicense.IsValid())
                return BadRequest(new ErrorResponse(newDriversLicense.Notifications));

            return CreatedAtAction("Get", new { id = newDriversLicense.DriversLicense.Id }, newDriversLicense);
        }
    }
}
