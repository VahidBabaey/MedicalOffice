using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
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

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] MedicalStaffWorkHoursProgramDTO dto)
    {
        var response = await _mediator.Send(new AddMedicalStaffWorkHoursProgramCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> UpdateMedicalStaffWorkHoursProgram([FromBody] MedicalStaffWorkHoursProgramDTO dto)
    {
        var response = await _mediator.Send(new EditMedicalStaffWorkHoursProgramCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteMedicalStaffWorkHoursProgramCommand() { MedicalStaffId = id });

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<MedicalStaffWorkHoursProgramListDTO>>> GetAll(Guid id)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffWorkHoursQuery() { MedicalStaffId = id });

        return Ok(response);
    }

}