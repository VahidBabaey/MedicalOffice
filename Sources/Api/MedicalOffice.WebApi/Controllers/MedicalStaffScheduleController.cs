using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Commands;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalStaffScheduleController : Controller
{
    private readonly IMediator _mediator;

    public MedicalStaffScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] MedicalStaffScheduleDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddMedicalStaffScheduleCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] MedicalStaffScheduleDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditMedicalStaffScheduleCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync([FromQuery] string medicalStaffId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteMedicalStaffScheduleCommand() { MedicalStaffId = Guid.Parse(medicalStaffId), OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MedicalStaffScheduleListDTO>>> GetByMedicalStaffId([FromQuery] string medicalStaffId, [FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffScheduleQuery() { DTO = dto, MedicalStaffId = Guid.Parse(medicalStaffId), OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}