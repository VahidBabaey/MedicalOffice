using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Features.MembershipFile.Requests.Commands;
using MedicalOffice.Application.Features.MembershipFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembershipController : Controller
{
    private readonly IMediator _mediator;

    public MembershipController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] MembershipDTO dto)
    {
        var response = await _mediator.Send(new AddMembershipCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<MembershipListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllMemberships() { DTO = dto});

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateMembershipDTO dto)
    {
        var response = await _mediator.Send(new EditMembershipCommand() { DTO = dto });

        return Ok(response);
    }
}