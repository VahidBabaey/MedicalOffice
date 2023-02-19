using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Features.DrugFile.Requests.Queries;
using MedicalOffice.Application.Features.PermissionFile.Requests.Commands;
using MedicalOffice.Application.Features.PermissionFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PermissionListDto>>> GetAll()
    {
        var response = await _mediator.Send(new GetPermissionsQuery() { });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("staff")]
    public async Task<ActionResult<List<PermissionListDto>>> GetStaffPermissions([FromQuery] string staffId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetStaffPermissionsQuery() { OfficeId = Guid.Parse(officeId), StaffId = Guid.Parse(staffId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> ChangeStaffPermission([FromBody] UpdateStaffPermissionDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new UpdateStaffPermissionsCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}