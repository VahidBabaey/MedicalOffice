using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InsuranceController : Controller
{
    private readonly IMediator _mediator;

    public InsuranceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] InsuranceDTO dto)
    {
        var response = await _mediator.Send(new AddInsuranceCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteInsuranceCommand() { InsuranceID = id });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateInsuranceDTO dto)
    {
        var response = await _mediator.Send(new EditInsuranceCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<InsuranceListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllInsuranceQuery() { DTO = dto });

        return Ok(response);
    }
}