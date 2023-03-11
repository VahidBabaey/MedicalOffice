using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Handlers.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Authorize]
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
    public async Task<ActionResult<Guid>> Create([FromBody] ServiceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddServiceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);

    }

    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteServiceCommand() { OfficeId = Guid.Parse(officeId), ServiceId = id });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateServiceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditServiceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("names")]
    public async Task<ActionResult<List<ServiceIdNameDTO>>> GetAllServiceNames([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllServiceNamesQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("section-services")]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAllBySectionId(Guid sectionId, [FromQuery] string officeId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllServicesBySectionIDQuery() { Dto = dto, SectionId = sectionId, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<ServiceListDTO>>> GetServicenBySearch([FromQuery] string name, [FromQuery] string officeId, [FromQuery] string sectionId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetServiceBySearchQuery() { Dto = dto, Name = name, OfficeId = Guid.Parse(officeId), SectionId = Guid.Parse(sectionId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("get-generic-codes")]
    public async Task<ActionResult<List<ServiceGenericCodeDTO>>> GetServiceGenericCodes([FromQuery] string name)
    {
        var response = await _mediator.Send(new GetServiceGenericCodsQuery() { Name = name });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [HttpGet("services-insuranceid")]
    public async Task<ActionResult<List<ServicesByInsuranceIdDTO>>> GetServicesByInsuranceId([FromQuery] string officeId, [FromQuery] string insuranceId)
    {
        var response = await _mediator.Send(new GetAllServicesByInsuranceIDQuery() { OfficeId = Guid.Parse(officeId), InsuranceId = Guid.Parse(insuranceId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}