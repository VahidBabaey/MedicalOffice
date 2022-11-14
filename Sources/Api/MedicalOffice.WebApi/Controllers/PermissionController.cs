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
        var response = await _mediator.Send(new AddPermissionCommand() { MedicalStaffid = id, DTO = dto });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdatePermissionDTO dto)
    {
        var response = await _mediator.Send(new EditPermissionCommand() {DTOUp = dto });

        return Ok(response);
    }
    [HttpGet("MedicalStaffs")]
    public async Task<ActionResult<List<MedicalStaffNameListDTO>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllMedicalStaffsName());

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<MedicalStaffNameListDTO>>> GetPermissionDetails(Guid id)
    {
        var response = await _mediator.Send(new GetPermissionDetailsofMedicalStaff() { UserOfficeRoleId = id});

        return Ok(response);
    }

}