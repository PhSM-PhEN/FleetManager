using System.Globalization;
using FleetManager.Application.UseCase.ToCompany.Delete;
using FleetManager.Application.UseCase.ToCompany.GetAll;
using FleetManager.Application.UseCase.ToCompany.GetById;
using FleetManager.Application.UseCase.ToCompany.Register;
using FleetManager.Application.UseCase.ToCompany.Update;
using FleetManager.communication.Requests;
using FleetManager.communication.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCompanyJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterCompanyUseCase useCase, 
        [FromBody] RequestCompanyJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListCompanyJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllCompanyUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseCompanyJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromServices] IGetByIdCompanyUseCase useCase)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }
        [HttpPut("{id}")]
        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromServices] IUpdateCompanyUseCase useCase,
         [FromBody] RequestCompanyJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromServices] IDeleteCompanyUseCase useCase)
        {
            await useCase.Execute(id);
            return NoContent();
        }

    }
}
