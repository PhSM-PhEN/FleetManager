using FleetManager.Application.UseCase.ToMaintenance.Close;
using FleetManager.Application.UseCase.ToMaintenance.Delete;
using FleetManager.Application.UseCase.ToMaintenance.GetAll;
using FleetManager.Application.UseCase.ToMaintenance.GetById;
using FleetManager.Application.UseCase.ToMaintenance.Register;
using FleetManager.Communication.Request.ToMaintenance;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaintenanceController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortMaintenanceJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterMaintenanceUseCase useCase, RequestMaintenanceJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponsePaginatedJson<ResponseShortMaintenanceJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllMaintenanceUseCase useCase, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await useCase.Execute(pageNumber, pageSize);
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseMaintenanceJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdMaintenanceUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] IDeleteMaintenanceUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
        [HttpPatch("{id}/Close")]
        [ProducesResponseType(typeof(ResponseMaintenanceJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Close([FromServices] ICloseMaintenanceUseCase useCase, [FromRoute]long id, [FromBody] RequestClosedMaintenanceJson request)
        {
            var response = await useCase.Execute(id, request);

            return Ok(response);
        }
    }
}
