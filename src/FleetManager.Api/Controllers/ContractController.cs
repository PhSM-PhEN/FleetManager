using FleetManager.Application.UseCase.ToContract.Register;
using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContractController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortContractJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterContractUseCase useCase, [FromBody] RequestContractJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
    }
}
