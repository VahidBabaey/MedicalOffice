using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Patient;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : Controller
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> Create([FromBody] PazireshDTO dto)
    {
        var response = await _mediator.Send(new AddPatientCommand() { Dto = dto });

        return Ok(response);
    }

    [HttpGet("GetAllPatients")]
    public async Task<ActionResult<List<PatientListDto>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllPatientsQuery() { Dto = dto });

        return Ok(response);
    }
    [HttpGet("GetAllPatientsByFileNumber")]
    public async Task<ActionResult<List<PatientListDto>>> GetAllByFileNumber([FromQuery] ListDto dto, string filenumber)
    {
        var response = await _mediator.Send(new GetPatientByFileNumberQuery() { Dto = dto, FileNumber = filenumber });

        return Ok(response);
    }
    [HttpGet("GetAllPatientsByNationalCode")]
    public async Task<ActionResult<List<PatientListDto>>> GetAllByNationalCode([FromQuery] ListDto dto, string nationalcode)
    {
        var response = await _mediator.Send(new GetPatientByNationalCodeQuery() { Dto = dto, NationalCode = nationalcode });

        return Ok(response);
    }
    [HttpGet("GetAllPatientsByPhoneNumber")]
    public async Task<ActionResult<List<PatientListDto>>> GetAllByPhoneNumber([FromQuery] ListDto dto, string phonenumber)
    {
        var response = await _mediator.Send(new GetPatientByPhoneNumberQuery() { Dto = dto, PhoneNumber = phonenumber });

        return Ok(response);
    }
    [HttpGet("GetAllPatientsByGenderIntrucerAcquaintedWay")]
    public async Task<ActionResult<List<PatientListDto>>> GetAllByGenderIntrucerAcquaintedWay([FromQuery] ListDto dto, int gender, int intrucer, int acquaintedway)
    {
        var response = await _mediator.Send(new GetAllPatientsByGenderIntrucerAcquaintedWayQuery() { Dto = dto, gender = gender, intrucer = intrucer , acquaintedway = acquaintedway });

        return Ok(response);
    }
}