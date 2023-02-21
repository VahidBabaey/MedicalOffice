using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Domain.Entities;
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

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteMembershipServiceCommand() { OfficeId = Guid.Parse(officeId), MembershipServiceId = id });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpGet("services")]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAll([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllServicesQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateMemberShipServiceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditServicetoMembershipCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAllServicesOfMemberShip(Guid memberShipId, [FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllServicesOfMemberShipQuery() { Dto = dto, MemberShipId = memberShipId, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [Authorize]
    [HttpGet("servicesbysearch")]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAllServicesOfMemberShipBySearch(Guid memberShipId, [FromQuery] string officeId, [FromQuery] ListDto dto, string name)
    {
        var response = await _mediator.Send(new GetAllServicesOfMemberShipQueryBySearch() { Dto = dto, MemberShipId = memberShipId, OfficeId = Guid.Parse(officeId), Name = name});

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}