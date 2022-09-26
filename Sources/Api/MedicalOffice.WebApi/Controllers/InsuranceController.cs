using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Insurance;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Dtos.Service;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InsuranceController : Controller
{
    private readonly IMediator _mediator;

    public InsuranceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateInsurance")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Guid>> Create([FromBody] InsuranceDTO dto)
    {
        var response = await _mediator.Send(new AddInsuranceCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpDelete("DeleteInsurance")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteInsuranceCommand() { InsuranceID = id });

        return Ok(response);
    }
    [HttpPost("UpdateInsurance")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Guid>> Update([FromBody] InsuranceDTO dto)
    {
        var response = await _mediator.Send(new EditInsuranceCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpGet("GetAllInsurances")]
    public async Task<ActionResult<List<InsuranceListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllInsuranceQuery() { Dto = dto });

        return Ok(response);
    }
}