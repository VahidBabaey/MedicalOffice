using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Domain.Enums;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] InsuranceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddInsuranceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpDelete("list-insurance")]
    public async Task<IActionResult> RemoveList([FromBody] InsuranceListIDDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteInsuranceListCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateInsuranceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditInsuranceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<InsuranceListDTO>>> GetAll([FromQuery] string officeId, [FromQuery] ListDto dto, [FromQuery] Order? order)
    {
        var response = await _mediator.Send(new GetAllInsuranceQuery() { Dto = dto, OfficeId = Guid.Parse(officeId), Order = order });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<List<InsuranceListDTO>>> GetInsuranceBySearch([FromQuery] string name, [FromQuery] string officeId, [FromQuery] ListDto dto, [FromQuery] Order? order)
    {
        var response = await _mediator.Send(new GetInsuranceBySearchQuery() { Dto = dto, Name = name, OfficeId = Guid.Parse(officeId), Order = order });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpGet("additionalinsurance-names")]
    public async Task<ActionResult<List<AdditionalInsuranceNamesDTO>>> GetAllAdditionalInsurances([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllAdditionalInsuranceNamesQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpGet("insurance-names")]
    public async Task<ActionResult<List<InsuranceNamesDTO>>> GetAllInsurances([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllInsuranceNamesQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}