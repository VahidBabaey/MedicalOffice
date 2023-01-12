using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
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
    public async Task<ActionResult<Guid>> Create([FromBody] ShiftDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddShiftCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteShiftCommand() { ShiftID = id, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateShiftDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditShiftCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<ShiftListDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllShiftsQuery() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

}