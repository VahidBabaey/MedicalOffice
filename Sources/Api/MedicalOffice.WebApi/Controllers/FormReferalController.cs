using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.FormIllnessDTO;
using MedicalOffice.Application.Dtos.FormReferalDTO;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Commands;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Queries;
using MedicalOffice.Application.Features.FormIllnessFile.Requests.Commands;
using MedicalOffice.Application.Features.FormIllnessFile.Requests.Queries;
using MedicalOffice.Application.Features.FormReferalFile.Requests.Commands;
using MedicalOffice.Application.Features.FormReferalFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormReferalController : Controller
{
    private readonly IMediator _mediator;

    public FormReferalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] AddFormReferalDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddFormReferalCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteFormReferalCommand() { FormReferalID = id, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateFormReferalDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditFormReferalCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<List<FormReferalListDTO>>> GetAll([FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GatAllFormReferalQuery() { Dto = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}