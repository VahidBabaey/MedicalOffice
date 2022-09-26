﻿using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Roledto;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Features.RoleFile.Handlers.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : Controller
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("GetRoles")]
    public async Task<ActionResult<List<RoleListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllRolesQuery() { Dto = dto });

        return Ok(response);
    }
}