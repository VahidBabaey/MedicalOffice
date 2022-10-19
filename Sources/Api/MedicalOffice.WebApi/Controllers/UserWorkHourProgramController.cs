using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.UserDTO;
using MedicalOffice.Application.Dtos.UserWorkHoursProgramFileDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.UserFile.Request.Commands;
using MedicalOffice.Application.Features.UserFile.Request.Queries;
using MedicalOffice.Application.Features.UserWorkHoursProgram.Handlers.Commands;
using MedicalOffice.Application.Features.UserWorkHoursProgram.Requests.Commands;
using MedicalOffice.Application.Features.UserWorkHoursProgram.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserWorkHourProgramController : Controller
{
    private readonly IMediator _mediator;

    public UserWorkHourProgramController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] UserWorkHoursProgramDTO dto)
    {
        var response = await _mediator.Send(new AddUserWorkHoursProgramCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> UpdateUserWorkHoursProgram([FromBody] UserWorkHoursProgramDTO dto)
    {
        var response = await _mediator.Send(new EditUserWorkHoursProgramCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteUserWorkHoursProgramCommand() { UserId = id });

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<UserWorkHoursProgramListDTO>>> GetAll(Guid id)
    {
        var response = await _mediator.Send(new GetAllUserWorkHoursQuery() { UserId = id });

        return Ok(response);
    }

}