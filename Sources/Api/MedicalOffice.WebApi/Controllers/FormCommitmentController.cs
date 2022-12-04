using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Commands;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormCommitmentController : Controller
{
    private readonly IMediator _mediator;

    public FormCommitmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

     [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] FormCommitmentDTO dto)
    {
        var response = await _mediator.Send(new AddFormCommitmentCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteFormCommitmentCommand() { FormCommitmentID = id });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateFormCommitmentDTO dto)
    {
        var response = await _mediator.Send(new EditFormCommitmentCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<FormCommitmentListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GatAllFormCommitmentQuery() { DTO = dto });

        return Ok(response);
    }
}