using FleetManager.Application.UseCase.ToRentalPlan.Delete;
using FleetManager.Application.UseCase.ToRentalPlan.GetAll;
using FleetManager.Application.UseCase.ToRentalPlan.GetById;
using FleetManager.Application.UseCase.ToRentalPlan.Register;
using FleetManager.Application.UseCase.ToRentalPlan.Update;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
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
        public async Task<IActionResult> Register([FromServices] IRegisterRentalPlanUseCase useCase, [FromBody] RequestRentalPlansJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListRentalPlanJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllRentalPlanUseCase useCase)
        {
            var respone = await useCase.Execute();
            if(respone.RentalPlans.Count != 0)
            {
                return Ok(respone);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseRentalPlanJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdRentalPlanUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromServices] IUpdateRentalPlanUseCase useCase, [FromRoute] long id, [FromBody] RequestRentalPlansJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete( [FromRoute] int id, [FromServices] IDeleteRentalPlanUseCase useCase)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}
