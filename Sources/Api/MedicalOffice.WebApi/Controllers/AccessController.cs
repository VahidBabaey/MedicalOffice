using MediatR;
using MedicalOffice.Application.Dtos.AccessDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffdto;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Features.AccessFile.Requests.Commands;
using MedicalOffice.Application.Features.AccessFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccessController : Controller
{
    private readonly IMediator _mediator;

    public AccessController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(string id, [FromBody] AccessDTO dto)
    {
        var response = await _mediator.Send(new AddAccessCommand() { userid = id, DTO = dto });
        //var response1 = await _mediator.Send(new AddAccessCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateAccessDTO dto)
    {
        var response = await _mediator.Send(new EditAccessCommand() {DTOUp = dto });
        //var response1 = await _mediator.Send(new AddAccessCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpGet("medicalstaffs")]
    public async Task<ActionResult<List<MedicalStaffNameListDTO>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllMedicalStaffsName());

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<MedicalStaffNameListDTO>>> GetAccessDetails(Guid id)
    {
        //id = Guid.Parse("b3002898-600c-42fb-e7f2-08da9b0eeca9");
        var response = await _mediator.Send(new GetAccessDetailsofMedicalStaff() { UserOfficeRoleId = id});

        return Ok(response);
    }

}