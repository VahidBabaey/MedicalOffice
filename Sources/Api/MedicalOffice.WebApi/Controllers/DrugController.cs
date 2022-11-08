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
using MedicalOffice.WebApi.Attributes;
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

    [HttpGet]
    public async Task<ActionResult<List<DrugListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetDrugQuery() { DTO = dto });
        return Ok(response);
        
    }

    [HttpGet("drug-features")]
    public async Task<ActionResult<List<DrugShapeListDTO>>> GetAllDrugs([FromQuery] ListDto dto)
    {
        var response1 = await _mediator.Send(new GetDrugShapeQuery() { DTO = dto });
        var response2 = await _mediator.Send(new GetDrugSectionQuery() { DTO = dto });
        var response3 = await _mediator.Send(new GetDrugUsageQuery() { DTO = dto });
        var response4 = await _mediator.Send(new GetDrugConsumptionQuery() { DTO = dto });

        return Ok(response4);
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