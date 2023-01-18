using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Features.MembershipFile.Requests.Commands;
using MedicalOffice.Application.Features.MembershipFile.Requests.Queries;
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

        return Ok(response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteMembershipCommand() { OfficeId = Guid.Parse(officeId), MembershipId = id });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MembershipListDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllMemberships() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateMembershipDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditMembershipCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
}