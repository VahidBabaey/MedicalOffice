using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceController : Controller
{
    private readonly IMediator _mediator;

    public ServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] ServiceDTO dto)
    {
        var response = await _mediator.Send(new AddServiceCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteServiceCommand() { ServiceId = id });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateServiceDTO dto)
    {
        var response = await _mediator.Send(new EditServiceCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAll([FromQuery] ListDto dto, Guid sectionId)
    {
        var response = await _mediator.Send(new GetAllServicesBySectionIDQuery() { DTO = dto, SectionId = sectionId });

        return Ok(response);
    }
}