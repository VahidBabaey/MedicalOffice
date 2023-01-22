using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Features.ShiftFile.Requests.Query;
using MedicalOffice.Application.Features.TariffFile.Requests.Commands;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

//[Authorize]
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

        return Ok(response);
    }
    [HttpGet("search")]
    public async Task<ActionResult<List<TariffListDTO>>> GetTariffsofService([FromQuery] string officeId, [FromQuery] string serviceId)
    {
        var response = await _mediator.Send(new GetAllTariffByServiceIDQuery() { OfficeId = Guid.Parse(officeId), ServiceId = Guid.Parse(serviceId) });

        return Ok(response);
    }
}