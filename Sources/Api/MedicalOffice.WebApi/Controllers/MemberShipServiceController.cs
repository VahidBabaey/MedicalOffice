using MediatR;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberShipServiceController : Controller
{
    private readonly IMediator _mediator;

    public MemberShipServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] MemberShipServiceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddServicetoMembershipCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [HttpGet("Services")]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAll()
    {
        var response = await _mediator.Send(new GetAllServicesQuery());

        return Ok(response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateMemberShipServiceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditServicetoMembershipCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAllServicesOfMemberShip(Guid memberShipId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllServicesOfMemberShipQuery() { MemberShipId = memberShipId, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
}