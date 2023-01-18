using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Application.Features.DrugIntractionFile.Requests.Commands;
using MedicalOffice.Application.Features.DrugIntractionFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] DrugIntractionDTO dto)
    {
        var response = await _mediator.Send(new AddDrugIntractionCommand() { DTO = dto });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteDrugIntractionCommand() { DrugIntractionID = id });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateDrugIntractionDTO dto)
    {
        var response = await _mediator.Send(new EditDrugIntractionCommand() { DTO = dto });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<DrugIntractionListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllDrugIntraction() { DTO = dto });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}