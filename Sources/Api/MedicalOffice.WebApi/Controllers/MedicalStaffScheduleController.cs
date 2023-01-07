using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Commands;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Queries;

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

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] MedicalStaffScheduleDTO dto)
    {
        var response = await _mediator.Send(new AddMedicalStaffScheduleCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpPatch]
    public async Task<ActionResult<Guid>> UpdateMedicalStaffSchedule([FromBody] MedicalStaffScheduleDTO dto)
    {
        var response = await _mediator.Send(new EditMedicalStaffScheduleCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid medicalStaffId)
    {
        var response = await _mediator.Send(new DeleteMedicalStaffScheduleCommand() { MedicalStaffId = medicalStaffId });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<MedicalStaffScheduleListDTO>>> GetAll(Guid medicalStaffId)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffScheduleQuery() { MedicalStaffId = medicalStaffId });

        return Ok(response);
    }
}