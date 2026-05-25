using FleetManager.Application.UseCase.ToRentalPlan.GetAll;
using FleetManager.Application.UseCase.ToRentalPlan.GetById;
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
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListAddressJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllRentalPlanUseCase useCase)
        {
            var respone = await useCase.Execute();
            return Ok(respone);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseListAddressJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromServices] IGetByIdRentalPlanUseCase useCase )
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }

    }
}
