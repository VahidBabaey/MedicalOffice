using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Application.Features.PictureFile.Requests.Commands;
using MedicalOffice.Application.Features.PictureFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PictureController : Controller
{
    private readonly IMediator _mediator;

    public PictureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] PictureUploadDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddPictureCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });
        return Ok(response);

    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeletePictureCommand() { PictureId = id, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PatientPicturesDTO>>> GetAll([FromQuery] Guid patientid, [FromQuery] ListDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllPicturesofPatientQuery() {Dto = dto ,PatientId = patientid, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
}