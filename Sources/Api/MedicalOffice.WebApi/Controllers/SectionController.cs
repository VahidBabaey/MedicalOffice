using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

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

     [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] SectionDTO dto)
    {
        var response = await _mediator.Send(new AddSectionCommand() { Dto = dto });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteSectionCommand() { SectionId = id });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateSectionDTO dto)
    {
        var response = await _mediator.Send(new EditSectionCommand() { Dto = dto });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<MembershipListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllSectionQuery() { Dto = dto });

        return Ok(response);
    }
}