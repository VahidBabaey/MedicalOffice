using MediatR;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.PermissionFile.Requests.Commands;
using MedicalOffice.Application.Features.PermissionFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;
using MedicalOffice.Application.Dtos.Permission;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionController : Controller
{
    private readonly IMediator _mediator;

    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(string id, [FromBody] PermissionDTO dto)
    {
        var response = await _mediator.Send(new AddPermissionCommand() { userid = id, DTO = dto });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdatePermissionDTO dto)
    {
        var response = await _mediator.Send(new EditPermissionCommand() { DTOUp = dto });

        return Ok(response);
    }
    [HttpGet("MedicalStaffs")]
    public async Task<ActionResult<List<MedicalStaffNameListDTO>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllUsersName());

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<MedicalStaffNameListDTO>>> GetPermissionDetails(Guid id)
    {
        var response = await _mediator.Send(new GetPermissionDetailsofUser() { UserOfficeRoleId = id });

        return Ok(response);
    }

    [HttpPatch("update-permission")]
    public async Task<ActionResult<List<Guid>>> UpdateMedicalStaffPermissions([FromBody] UpdateMedicalStaffPermissionsDTO dto, [FromQuery] Guid officeId)
    {
        var response = await _mediator.Send(new UpdateMedicalStaffCommand() { DTO = dto, OffceId = officeId });

        return Ok(response);
    }
}