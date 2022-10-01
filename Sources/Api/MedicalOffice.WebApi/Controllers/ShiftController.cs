using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Dtos.Shift;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ShiftFile.Requests.Command;
using MedicalOffice.Application.Features.ShiftFile.Requests.Query;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftController : Controller
{
    private readonly IMediator _mediator;

    public ShiftController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] ShiftDTO dto)
    {
        var response = await _mediator.Send(new AddShiftCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteShiftCommand() { ShiftID = id });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateShiftDTO dto)
    {
        var response = await _mediator.Send(new EditShiftCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<MembershipListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllShiftsQuery() { DTO = dto });

        return Ok(response);
    }

}