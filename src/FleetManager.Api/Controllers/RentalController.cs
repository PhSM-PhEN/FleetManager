using FleetManager.Application.UseCase.ToRental.GetAll;
using FleetManager.Application.UseCase.ToRental.GetById;
using FleetManager.Application.UseCase.ToRental.Register;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentalController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRentalJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterRentalUseCase useCase, [FromBody] RequestRentJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListRentalJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllRentalUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseRentalInfoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdRentalUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
    }
}
