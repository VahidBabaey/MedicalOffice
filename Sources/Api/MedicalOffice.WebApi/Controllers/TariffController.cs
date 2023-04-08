using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.TariffFile.Requests.Commands;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TariffController : Controller
{
    private readonly IMediator _mediator;

    public TariffController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] TariffDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddServiceTariffCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet]
    public async Task<ActionResult<List<TariffListDTO>>> GetTariffsOfService([FromQuery] string officeId, [FromQuery] ServiceIdDTO serviceId, [FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllTariffByServiceIDQuery() { Dto = dto, OfficeId = Guid.Parse(officeId), ServiceId = serviceId });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpDelete("list-tariff")]
    public async Task<IActionResult> RemoveList([FromBody] TariffListIdDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteTariffListCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("generic-code-tariff")]
    public async Task<ActionResult<int>> GetGenericCodeTariff([FromQuery] string genericCode, [FromQuery] string insuranceId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetGenericCodeTariffQuery() { GenericCode = genericCode , InsuranceId = Guid.Parse(insuranceId) , OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}