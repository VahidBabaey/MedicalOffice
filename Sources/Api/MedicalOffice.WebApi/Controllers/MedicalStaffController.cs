using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
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
    public async Task<ActionResult<Guid>> Create([FromBody] MedicalStaffDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddMedicalStaffCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> UpdateMedicalStaff([FromBody] UpdateMedicalStaffDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditMedicalStaffCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteMedicalStaffCommand() { MedicalStaffId = id, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<MedicalStaffListDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllMedicalStaffs() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [Permission(BasicInfoPermissions.GetAllDetails)]
    [HttpPatch("permissions")]
    public async Task<ActionResult<List<Guid>>> UpdateMedicalStaffPermissions([FromBody] MedicalStaffPermissionsDTO dto, [FromQuery] Guid officeId)
    {
        var response = await _mediator.Send(new UpdateMedicalStaffCommand() { DTO = dto, OffceId = officeId });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}