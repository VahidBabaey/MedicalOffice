using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Dtos.Specialization;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.SpecializationFile.Requests.Commands;
using MedicalOffice.Application.Features.SpecializationFile.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecializationController : Controller
{
    private readonly IMediator _mediator;

    public SpecializationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> Create([FromBody] SpecializationDTO dto)
    {
        var response = await _mediator.Send(new AddSpecializationCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpGet("GetSpecializations")]
    public async Task<ActionResult<List<MembershipListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllSpecializationsQuery() { DTO = dto });

        return Ok(response);
    }
}