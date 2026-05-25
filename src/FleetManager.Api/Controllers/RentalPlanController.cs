using FleetManager.Application.UseCase.ToRentalPlan.Register;
using FleetManager.communication.Requests;
using FleetManager.communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalPlanController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRentalPlanJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterRentalPlanUseCase useCase, [FromBody] RequestRentalPlansJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

    }
}
