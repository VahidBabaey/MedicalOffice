using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Domain.Enums;
using MedicalOffice.WebApi.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalStaffController : Controller
{
    private readonly IMediator _mediator;

    public MedicalStaffController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] MedicalStaffDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddMedicalStaffCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> UpdateMedicalStaff([FromBody] UpdateMedicalStaffDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditMedicalStaffCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteMedicalStaffCommand() { MedicalStaffId = id, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MedicalStaffListDTO>>> GetAll([FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffsQuery() {Dto = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [Permission(BasicInfoPermissions.GetAllDetails)]
    [HttpPatch("permissions")]
    public async Task<ActionResult<List<Guid>>> UpdateMedicalStaffPermissions([FromBody] MedicalStaffPermissionsDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new UpdateMedicalStaffCommand() { DTO = dto, OffceId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<List<MedicalStaffListDTO>>> GetMedicalStaffBySearch([FromQuery] string name, [FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetMedicalStaffBySearchQuery() { Dto = dto, Name = name, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("titles")]
    public async Task<ActionResult<Title>> GetAllTitles([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffTitleQuery { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("Doctors")]
    public async Task<ActionResult<MedicalStaffNameListDTO>> GetDoctorAndExperts([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllOfficeDoctorsQuery { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}