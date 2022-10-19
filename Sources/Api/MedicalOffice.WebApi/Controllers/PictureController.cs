using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.PictureFile.Requests.Commands;
using MedicalOffice.Application.Features.PictureFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

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

     [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] PictureUploadDTO dto)
    {
        var response = await _mediator.Send(new AddPictureCommand() { DTO = dto });
        return Ok(response);

    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeletePictureCommand() { PictureId = id });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientPicturesDTO>>> GetAll([FromQuery] Guid patientid)
    {
        var response = await _mediator.Send(new GetAllPicturesofPatientQuery() { PatientId = patientid });

        return Ok(response);
    }
}