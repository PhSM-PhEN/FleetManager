using FleetManager.Application.UseCase.ToVehicle.Delete;
using FleetManager.Application.UseCase.ToVehicle.GetAll;
using FleetManager.Application.UseCase.ToVehicle.GetById;
using FleetManager.Application.UseCase.ToVehicle.Register;
using FleetManager.Application.UseCase.ToVehicle.Update.UpdateKm;
using FleetManager.Application.UseCase.ToVehicle.Update.UpdateVehicle;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
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
        [ProducesResponseType(typeof(ResponseRegisterVehicleJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Register([FromServices] IRegisterVehicleUseCase useCase,
            [FromBody] RequestVehicleJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseListVehicleJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllVehicleUseCase useCase,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10 )
        {

            var response = await useCase.Execute(pageNumber, pageSize);
            return Ok(response);
            
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseVehicleByIdJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdVehicleUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);

        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromServices] IUpdateVehicleUseCase useCase, [FromRoute] long id, [FromBody] RequestUpdateVehicleJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }
        [HttpPut("kmtotal/{id}")]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseVehicleJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateKm([FromServices] IUpdateVehicleKmUseCase useCase, [FromRoute] long id, [FromBody] RequestVehicleUpdateCurrentMileageJson request)
        {
            var response = await useCase.Execute(id, request);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromServices] IDeleteVehicleUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}
