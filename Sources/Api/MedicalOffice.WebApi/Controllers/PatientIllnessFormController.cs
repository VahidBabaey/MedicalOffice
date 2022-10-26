using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientIllnessFormController : Controller
{
    private readonly IMediator _mediator;

    public PatientIllnessFormController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("illnessReasons")]
    public async Task<ActionResult<List<BasicInfoDetailListDTO>>> GetPatientIllnessReasons()
    {
        var response = await _mediator.Send(new GetAlliillnessReasonsQuery());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] PatientIllnessFormDTO dto)
    {
        var response = await _mediator.Send(new AddPatientIllnessFormCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientIllnessFormListDTO>>> GetAll([FromQuery] ListDto dto, Guid patientid)
    {
        var response = await _mediator.Send(new GetAllPatientIllnessFormQuery() { DTO = dto, PatientId = patientid });

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeletePatientIllnessFormCommand() { PatientIllnessFormId = id });

        return Ok(response);
    }

}