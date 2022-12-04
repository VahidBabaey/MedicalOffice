using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SectionController : Controller
{
    private readonly IMediator _mediator;

    public SectionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] SectionDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddSectionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteSectionCommand() { SectionId = id, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateSectionDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditSectionCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<MembershipListDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllSectionQuery() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
}