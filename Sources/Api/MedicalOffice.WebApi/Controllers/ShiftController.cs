using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Features.DrugFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ShiftFile.Requests.Command;
using MedicalOffice.Application.Features.ShiftFile.Requests.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Authorize]
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

    [HttpDelete("list-shift")]
    public async Task<IActionResult> RemoveList([FromBody] ShiftListIDDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteShiftListCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateShiftDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditShiftCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<ShiftListDTO>>> GetAll([FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllShiftsQuery() {Dto = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpGet("Search")]
    public async Task<ActionResult<List<ShiftListDTO>>> GetShiftBySearch([FromQuery] string name, [FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetShiftBySearchQuery() {Dto = dto, Name = name, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
}