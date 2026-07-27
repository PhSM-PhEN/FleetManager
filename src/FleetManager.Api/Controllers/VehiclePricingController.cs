using FleetManager.Application.UseCase.ToVehiclePricing.GetByVehicleId;
using FleetManager.Application.UseCase.ToVehiclePricing.Register;
using FleetManager.Application.UseCase.ToVehiclePricing.Update;
using FleetManager.Communication.Request.ToVehiclePricing;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToVehiclePricing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehiclePricingController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseVehiclePricingJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromServices] IRegisterVehiclePricingUseCase useCase, [FromBody] RequestVehiclePricingJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet("{vehicleId}")]
        [ProducesResponseType(typeof(ResponseVehiclePricingJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByVehicleId([FromServices] IGetByVehicleIdVehiclePricingUseCase useCase, [FromRoute] long vehicleId)
        {
            var response = await useCase.Execute(vehicleId);
            return Ok(response);
        }

        [HttpPut("{vehicleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateVehiclePricingUseCase useCase, [FromRoute] long vehicleId, [FromBody] RequestVehiclePricingJson request)
        {
            await useCase.Execute(vehicleId, request);
            return NoContent();
        }
    }
}
