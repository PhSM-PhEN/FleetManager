using FleetManager.Application.UseCase.ToCategory.Delete;
using FleetManager.Application.UseCase.ToCategory.GetAll;
using FleetManager.Application.UseCase.ToCategory.GetById;
using FleetManager.Application.UseCase.ToCategory.Register;
using FleetManager.Application.UseCase.ToCategory.Update;
using FleetManager.communication.Requests;
using FleetManager.communication.Resposnes;
using FleetManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortCategoryJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterCategoryUseCase useCase,
            [FromBody] RequestCategoryJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseShortCategoryJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllCategoyUseCase useCase)
        {
            var response = await useCase.Execute();
            if (response.Categories.Count != 0)
            {
                return Ok(response);
            }


            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseCategoryJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdCategoryUseCase useCase, [FromRoute] int id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromServices] IUpdateCategoryUseCase useCase, [FromRoute] int id, RequestCategoryJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromServices] IDeleteCategoryUseCase useCase, [FromRoute] int id)
        {
            await useCase.Execute(id);
            return NoContent();


        }
    }

}
