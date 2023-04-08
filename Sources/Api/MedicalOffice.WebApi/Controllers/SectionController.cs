using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ShiftFile.Requests.Query;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SectionController : Controller
{
    private readonly IMediator _mediator;

    public SectionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] AddSectionDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddSectionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteSectionCommand() { SectionId = id, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateSectionDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditSectionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ShiftListDTO>>> GetAll([FromQuery] string officeId, [FromQuery] ListDto dto, [FromQuery] Order? order)
    {
        var response = await _mediator.Send(new GetAllSectionQuery() { Dto = dto, OfficeId = Guid.Parse(officeId), Order = order });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<List<SectionListDTO>>> GetSectionBySearch([FromQuery] string? name, [FromQuery] string officeId, [FromQuery] ListDto dto, [FromQuery] Order? order)
    {
        var response = await _mediator.Send(new GetSectionBySearchQuery() { Dto = dto, Name = name, OfficeId = Guid.Parse(officeId), Order = order });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpGet("names")]
    public async Task<ActionResult<List<SectionNamesListDTO>>> GetSectionNames([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetSectionNamesQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}