﻿using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffdto;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Features.MdicalStaffFile.Request.Commands;
using MedicalOffice.Application.Features.MdicalStaffFile.Request.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

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

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> Create([FromBody] MedicalStaffDTO dto)
    {
        var response = await _mediator.Send(new AddMedicalStaffCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpPost("Update")]
    public async Task<ActionResult<Guid>> UpdateUser([FromBody] MedicalStaffDTO dto)
    {
        var response = await _mediator.Send(new EditMedicalStaffCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpDelete("Delete")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteMedicalStaffCommand() { MedicalStaffId = id });

        return Ok(response);
    }
    [HttpGet("GetMedicallStaffs")]
    public async Task<ActionResult<List<MedicalStaffListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffs() { DTO = dto });

        return Ok(response);
    }
}