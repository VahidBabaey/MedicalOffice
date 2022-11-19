using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.WebApi.Attributes;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] MedicalStaffDTO dto)
    {
        var response = await _mediator.Send(new AddMedicalStaffCommand() { DTO = dto });

        return Ok(response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> UpdateMedicalStaff([FromBody] UpdateMedicalStaffDTO dto)
    {
        var response = await _mediator.Send(new EditMedicalStaffCommand() { DTO = dto });

        return Ok(response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteMedicalStaffCommand() { MedicalStaffId = id });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MedicalStaffListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffs() { DTO = dto });

        return Ok(response);
    }

    [Authorize]
    [
        Permission("DoctorPermissionLightPen"),
        Permission("dgdg"),
        Permission("hkxdhkjh")
      ]
    [HttpPatch("permissions")]
    public async Task<ActionResult<List<Guid>>> UpdateMedicalStaffPermissions([FromBody] MedicalStaffPermissionsDTO dto, [FromQuery] Guid officeId)
    {
        var response = await _mediator.Send(new UpdateMedicalStaffCommand() { DTO = dto, OffceId = officeId });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}