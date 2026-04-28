using FleetManager.Application.UseCase.ToUser.Delete;
using FleetManager.Application.UseCase.ToUser.GetUser;
using FleetManager.Application.UseCase.ToUser.Register;
using FleetManager.Application.UseCase.ToUser.Update;
using FleetManager.communication.Requests.ToUser;
using FleetManager.communication.Resposnes;
using FleetManager.communication.Resposnes.ToUsers;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser([FromServices] IGetUserProfileUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser([FromServices] IDeleteUserAccountUseCase useCase)
        {
            await useCase.Execute();
            return NoContent();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser([FromServices] IUpdateProfileUseCase useCase, [FromBody] RequestUpdateUserJson request)
        {
            await useCase.Execute(request);
            return NoContent();
        }
    }
}
