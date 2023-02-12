using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.RoleDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.RoleFile.Handlers.Queries;
using MedicalOffice.Application.Features.RoleFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    public async Task<ActionResult<List<RoleListDTO>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllRolesQuery());

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("situation")]
    public async Task<ActionResult<List<RoleSituationDTO>>> GetRoleSituation([FromQuery] Guid roleId)
    {
        var response = await _mediator.Send(new GetRoleSituationQuery() { RoleId = roleId });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}