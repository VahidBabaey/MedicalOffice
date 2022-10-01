using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugD;
using MedicalOffice.Application.Dtos.DrugIntractionD;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Features.DrugFile.Handlers.Queries;
using MedicalOffice.Application.Features.DrugFile.Requests.Commands;
using MedicalOffice.Application.Features.DrugFile.Requests.Queries;
using MedicalOffice.Application.Features.DrugIntractionFile.Requests.Commands;
using MedicalOffice.Application.Features.DrugIntractionFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DrugIntractionController : Controller
{
    private readonly IMediator _mediator;

    public DrugIntractionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] DrugIntractionDTO dto)
    {
        var response = await _mediator.Send(new AddDrugIntractionCommand() { DTO = dto});

        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteDrugIntractionCommand() { DrugIntractionID = id });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateDrugIntractionDTO dto)
    {
        var response = await _mediator.Send(new EditDrugIntractionCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<DrugIntractionListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllDrugIntraction() { DTO = dto });
        return Ok(response);
    }

}