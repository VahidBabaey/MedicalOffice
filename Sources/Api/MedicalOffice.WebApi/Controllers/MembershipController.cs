using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Features.Experiment.Requests.Queries;
using MedicalOffice.Application.Features.MembershipFile.Requests.Commands;
using MedicalOffice.Application.Features.MembershipFile.Requests.Queries;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembershipController : Controller
{
    private readonly IMediator _mediator;

    public MembershipController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] MembershipDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddMembershipCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }


    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateMembershipDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditMembershipCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteMembershipCommand() { OfficeId = Guid.Parse(officeId), MembershipId = id });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MembershipListDTO>>> GetAll([FromQuery] string officeId, [FromQuery] ListDto dto, [FromQuery] Order? order)
    {
        var response = await _mediator.Send(new GetAllMemberships() { Dto = dto, OfficeId = Guid.Parse(officeId), Order = order });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<List<MembershipListDTO>>> GetMembershipBySearch([FromQuery] string name, [FromQuery] string officeId, [FromQuery] ListDto dto, [FromQuery] Order? order)
    {
        var response = await _mediator.Send(new GetMembershipBySearchQuery() { Dto = dto, Name = name, OfficeId = Guid.Parse(officeId), Order = order });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("names")]
    public async Task<ActionResult<List<MembershipsNamesDTO>>> GetAllMembershipsNames([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllMembershipsNames() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}