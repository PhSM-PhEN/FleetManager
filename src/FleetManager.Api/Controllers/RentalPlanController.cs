using FleetManager.Application.UseCase.ToVehiclePricing.GetByVehicleId;
using FleetManager.Application.UseCase.ToVehiclePricing.Register;
using FleetManager.Application.UseCase.ToVehiclePricing.Update;
using FleetManager.Communication.Request.ToRentalPlan;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToRentalPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentalPlanController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRentalPlanJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromServices] IRegisterRentalPlanUseCase useCase, [FromBody] RequestRentalPlanJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet("{vehicleId}")]
        [ProducesResponseType(typeof(ResponseRentalPlanJson), StatusCodes.Status200OK)]
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
        public async Task<IActionResult> Update([FromServices] IUpdateRentalPlanUseCase useCase, [FromRoute] long vehicleId, [FromBody] RequestRentalPlanJson request)
        {
            await useCase.Execute(vehicleId, request);
            return NoContent();
        }
    }
}
