using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.SpecializationDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.SpecializationFile.Requests.Commands;
using MedicalOffice.Application.Features.SpecializationFile.Requests.Queries;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SpecializationController : Controller
{
    private readonly IMediator _mediator;

    public SpecializationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] SpecializationDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddSpecializationCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<SpecializationListDTO>>> GetAll([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllSpecializationsQuery() {OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
}