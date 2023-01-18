using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Features.DrugFile.Handlers.Queries;
using MedicalOffice.Application.Features.DrugFile.Requests.Commands;
using MedicalOffice.Application.Features.DrugFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DrugController : Controller
{
    private readonly IMediator _mediator;

    public DrugController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<DrugListDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetDrugQuery() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("drug-shape")]
    public async Task<ActionResult<List<DrugShapeListDTO>>> GetDrugsShape([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetDrugShapeQuery() { DTO = dto });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("drug-consumption")]
    public async Task<ActionResult<List<DrugConsumptionListDTO>>> GetDrugsConsumption([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetDrugConsumptionQuery() { DTO = dto });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("drug-usage")]
    public async Task<ActionResult<List<DrugUsageListDTO>>> GetDrugsUsage([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetDrugUsageQuery() { DTO = dto });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("drug-section")]
    public async Task<ActionResult<List<DrugSectionListDTO>>> GetDrugsSection([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetDrugSectionQuery() { DTO = dto });

        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] DrugDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddDrugCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateDrugDTO dto, [FromQuery] string officeId)
    {

        var response = await _mediator.Send(new EditDrugCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteDrugCommand() { DrugId = id, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet("Search")]
    public async Task<ActionResult<List<DrugListDTO>>> GetDrugBySearch([FromQuery] string name, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetDrugBySearchQuery() { Name = name, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}