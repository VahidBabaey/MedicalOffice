using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.FormIllnessDTO;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Commands;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Queries;
using MedicalOffice.Application.Features.FormIllnessFile.Requests.Commands;
using MedicalOffice.Application.Features.FormIllnessFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormIllnessController : Controller
{
    private readonly IMediator _mediator;

    public FormIllnessController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] AddFormIllnessDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddFormIllnessCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteFormIllnessCommand() { FormIllnessID = id, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateFormIllnessDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditFormIllnessCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<List<FormIllnessListDTO>>> GetAll([FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GatAllFormIllnessQuery() {Dto = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}