using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.DrugFile.Handlers.Queries;
using MedicalOffice.Application.Features.DrugFile.Requests.Commands;
using MedicalOffice.Application.Features.DrugFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

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

    [HttpGet]
    public async Task<ActionResult<List<DrugListDTO>>> GetAll([FromQuery] ListDto dto)
    {

        var response = await _mediator.Send(new GetDrugQuery() { DTO = dto });

        return Ok(response);
        
    }

    [HttpGet("drug-shape")]
    public async Task<ActionResult<List<DrugShapeListDTO>>> GetDrugsShape([FromQuery] ListDto dto)
    {

        var response = await _mediator.Send(new GetDrugShapeQuery() { DTO = dto });

        return Ok(response);

    }
    [HttpGet("drug-consumption")]
    public async Task<ActionResult<List<DrugShapeListDTO>>> GetDrugsConsumption([FromQuery] ListDto dto)
    {

        var response = await _mediator.Send(new GetDrugConsumptionQuery() { DTO = dto });

        return Ok(response);

    }
    [HttpGet("drug-usage")]
    public async Task<ActionResult<List<DrugShapeListDTO>>> GetDrugsUsage([FromQuery] ListDto dto)
    {

        var response = await _mediator.Send(new GetDrugUsageQuery() { DTO = dto });

        return Ok(response);

    }
    [HttpGet("drug-section")]
    public async Task<ActionResult<List<DrugShapeListDTO>>> GetDrugsSection([FromQuery] ListDto dto)
    {

        var response = await _mediator.Send(new GetDrugSectionQuery() { DTO = dto });

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] DrugDTO dto)
    {

        var response = await _mediator.Send(new AddDrugCommand() { DTO = dto });

        return Ok(response);

    }

    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateDrugDTO dto)
    {

        var response = await _mediator.Send(new EditDrugCommand() { DTO = dto });

        return Ok(response);

    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {

        var response = await _mediator.Send(new DeleteDrugCommand() { DrugId = id });

        return Ok(response);

    }
}