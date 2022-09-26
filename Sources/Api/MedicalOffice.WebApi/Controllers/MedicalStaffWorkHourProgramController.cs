using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffdto;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFile;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Features.MdicalStaffFile.Request.Commands;
using MedicalOffice.Application.Features.MdicalStaffFile.Request.Queries;
using MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Handlers.Commands;
using MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Requests.Commands;
using MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalStaffWorkHourProgramController : Controller
{
    private readonly IMediator _mediator;

    public MedicalStaffWorkHourProgramController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> Create([FromBody] MedicalStaffWorkHoursProgramDTO dto)
    {
        var response = await _mediator.Send(new AddMedicalStaffWorkHoursProgramCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpPost("Update")]
    public async Task<ActionResult<Guid>> UpdateMedicalStaffWorkHoursProgram([FromBody] MedicalStaffWorkHoursProgramDTO dto)
    {
        var response = await _mediator.Send(new EditMedicalStaffWorkHoursProgramCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpDelete("Delete")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteMedicalStaffWorkHoursProgramCommand() { MedicalStaffId = id });

        return Ok(response);
    }
    [HttpGet("Get")]
    public async Task<ActionResult<List<MedicalStaffWorkHoursProgramListDTO>>> GetAll(Guid id)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffWorkHoursQuery() { MedicalStaffId = id });

        return Ok(response);
    }

}