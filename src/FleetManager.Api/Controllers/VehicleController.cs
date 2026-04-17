using FleetManager.Application.UseCase.ToVehicle.Delete;
using FleetManager.Application.UseCase.ToVehicle.GetAll;
using FleetManager.Application.UseCase.ToVehicle.GetById;
using FleetManager.Application.UseCase.ToVehicle.Register;
using FleetManager.Application.UseCase.ToVehicle.Update.UpdateKm;
using FleetManager.Application.UseCase.ToVehicle.Update.UpdateVehicle;
using FleetManager.communication.Requests.ToVehicle;
using FleetManager.communication.Resposnes;
using FleetManager.communication.Resposnes.ToVehicle;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [ProducesResponseType(typeof(ResponseAllVehicleJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllVehicleUseCase useCase)
        {
            var response = await useCase.Execute();
            if (response.Vehicle.Count != 0) 
            {
                return Ok(response);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseVehicleByIdJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdVehicleUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);

        }
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromServices] IUpdateVehicleUseCase useCase, [FromRoute] long id, RequestVehicleJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }
        [HttpPut]
        [Route("kmtotal/{id}")]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseVehicleJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateKm([FromServices] IUpdateVehicleKmUseCase useCase, [FromRoute] long id, [FromBody] RequestVehicleUpdateCurrentMiliageJson request)
        {
            var response = await useCase.Execute(id, request);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromServices] IDeleteVehicleUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}
