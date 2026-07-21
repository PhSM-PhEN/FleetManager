using FleetManager.Application.UseCase.ToTenant.GetAll;
using FleetManager.Application.UseCase.ToTenant.GetById;
using FleetManager.Application.UseCase.ToTenant.Register;
using FleetManager.Communication.Request.ToTenant;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToRenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TenantController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseTenantJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterTenantUseCase useCase, [FromBody] RequestTenantJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseTenantJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllTenantUseCase useCase, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await useCase.Execute(pageNumber, pageSize);
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseInfoTenantJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdTenantUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
    }
}
