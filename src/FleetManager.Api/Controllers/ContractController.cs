using FleetManager.Application.UseCase.ToContract.Activate;
using FleetManager.Application.UseCase.ToContract.Cancel;
using FleetManager.Application.UseCase.ToContract.Delete;
using FleetManager.Application.UseCase.ToContract.DetectOverdue;
using FleetManager.Application.UseCase.ToContract.FinishUp;
using FleetManager.Application.UseCase.ToContract.GenerateDocument;
using FleetManager.Application.UseCase.ToContract.GetAll;
using FleetManager.Application.UseCase.ToContract.GetById;
using FleetManager.Application.UseCase.ToContract.Preview;
using FleetManager.Application.UseCase.ToContract.Register;
using FleetManager.Application.UseCase.ToContract.Renew;
using FleetManager.Application.UseCase.ToContract.Update;
using FleetManager.Communication.Request.ToContract;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToContract;
using FleetManager.Domain.Enum;
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
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromServices] IRegisterContractUseCase useCase, [FromBody] RequestContractJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
        [HttpPost("Preview")]
        [ProducesResponseType(typeof(ResponseShortContractJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Preview([FromServices] IPreviewContractUseCase useCase, [FromBody] RequestPreviewContractJson request)
        {
            var respose = await useCase.Execute(request);
            return Ok(respose);
        }    
        

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseContractJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdContractUseCase useCase ,[FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponsePaginatedJson<ResponseShortContractJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllContractUseCase useCase, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await useCase.Execute(pageNumber, pageSize);
            return Ok(response);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateContractUseCase useCase, [FromRoute] long id, [FromBody] RequestUpdateContractJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] IDeleteContractUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpPatch("{id}/Cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cancel([FromServices] ICancelContractUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpPatch("{id}/Activate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Activate([FromServices] IActivateContractUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpPatch("{id}/FinishUp")]
        [ProducesResponseType(typeof(ResponseFinishUpContractJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FinishUp([FromServices] IFinishUpContractUseCase useCase, [FromRoute] long id, [FromBody] RequestFinishUpContractJson request)
        {
            var response = await useCase.Execute(id, request);
            return Ok(response);
        }
        [HttpPost("DetectOverdue")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(typeof(ResponseDetectOverdueContractsJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> DetectOverdue([FromServices] IDetectOverdueContractsUseCase useCase)
        {
            var totalMarked = await useCase.Execute();
            return Ok(new ResponseDetectOverdueContractsJson { TotalContractsMarkedAsOverdue = totalMarked });
        }

        // Gera (e congela) o texto do contrato a partir do ContractTemplate ativo no momento.
        [HttpPost("{id}/Document")]
        [ProducesResponseType(typeof(ResponseContractDocumentJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GenerateDocument([FromServices] IGenerateContractDocumentUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }

        [HttpPost("{id}/Renew")]
        [ProducesResponseType(typeof(ResponseShortContractJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Renew([FromServices] IRenewContractUseCase useCase, [FromRoute] long id, [FromBody] RequestRenewContractJson request)
        {
            var response = await useCase.Execute(id, request);
            return Created(string.Empty, response);
        }
    }
}
