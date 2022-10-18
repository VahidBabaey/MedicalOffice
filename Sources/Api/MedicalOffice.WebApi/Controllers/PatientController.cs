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

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] PazireshDTO dto)
    {
        var response = await _mediator.Send(new AddPatientCommand() { Dto = dto });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<ActionResult<Guid>> Delete(Guid patientId)
    {
        var response = await _mediator.Send(new DeletePatientCommand() { PatientId = patientId });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateAddPatientDto dto, Guid patientId)
    {
        var response = await _mediator.Send(new EditPatientCommand() { Dto = dto , PatientId = patientId});

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientListDto>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllPatientsQuery() { Dto = dto });

        return Ok(response);
    }
    [HttpGet("Search")]
    public async Task<ActionResult<List<PatientListDto>>> GetBySearch([FromQuery] ListDto dto, string? nationalcode, string? filenumber, string? fullname, string? phonenumber)
    {
        var response = await _mediator.Send(new GetPatientBySearchQuery() { Dto = dto, nationalcode = nationalcode, phonenumber = phonenumber, filenumber = filenumber, fullname = fullname });

        return Ok(response);
    }
    //[HttpGet("filenumber")]
    //public async Task<ActionResult<List<PatientListDto>>> GetAllByFileNumber([FromQuery] ListDto dto, string filenumber)
    //{
    //    var response = await _mediator.Send(new GetPatientByFileNumberQuery() { Dto = dto, FileNumber = filenumber });

    //    return Ok(response);
    //}
    //[HttpGet("nationalcode")]
    //public async Task<ActionResult<List<PatientListDto>>> GetAllByNationalCode([FromQuery] ListDto dto, string nationalcode)
    //{
    //    var response = await _mediator.Send(new GetPatientByNationalCodeQuery() { Dto = dto, NationalCode = nationalcode });

    //    return Ok(response);
    //}
    //[HttpGet("phonenumber")]
    //public async Task<ActionResult<List<PatientListDto>>> GetAllByPhoneNumber([FromQuery] ListDto dto, string phonenumber)
    //{
    //    var response = await _mediator.Send(new GetPatientByPhoneNumberQuery() { Dto = dto, PhoneNumber = phonenumber });

    //    return Ok(response);
    //}
    //[HttpGet("genderintruceracquaintedWay")]
    //public async Task<ActionResult<List<PatientListDto>>> GetAllByGenderIntrucerAcquaintedWay([FromQuery] ListDto dto, int gender, int intrucer, int acquaintedway)
    //{
    //    var response = await _mediator.Send(new GetAllPatientsByGenderIntrucerAcquaintedWayQuery() { Dto = dto, gender = gender, intrucer = intrucer , acquaintedway = acquaintedway });

    //    return Ok(response);
    //}
}