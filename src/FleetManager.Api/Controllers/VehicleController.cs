using FleetManager.Application.UseCase.ToVehicle.Delete;
using FleetManager.Application.UseCase.ToVehicle.GetAll;
using FleetManager.Application.UseCase.ToVehicle.GetById;
using FleetManager.Application.UseCase.ToVehicle.Register;
using FleetManager.Application.UseCase.ToVehicle.Update;
using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToVehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseVehicleRegisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterVehicleUseCase useCase, [FromBody] RequestVehicleJson request)
        {
            var response = await useCase.Execute(request);
            
            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponsePaginatedJson<ResponseShortVehicleJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllVehicleUseCase useCase, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await useCase.Execute(pageNumber, pageSize);
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseVehicleJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdVehicleUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateMileageVehicleUseCase useCase, [FromBody] RequestMileageVehicleJson request , long id)
        {
            await useCase.Execute(id, request);
            return NoContent();

        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] IDeleteVehicleUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}
