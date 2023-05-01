using MediatR;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.CashFile.Request.Commands;
using MedicalOffice.Application.Features.CashFile.Request.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MedicalOffice.WebApi.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CashController : Controller
{
    private readonly IMediator _mediator;

    public CashController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Pos")]
    public async Task<ActionResult<Guid>> CreatePos([FromBody] CashPosDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashPosCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
    [HttpPost("Money")]
    public async Task<ActionResult<Guid>> CreateMoney([FromBody] CashMoneyDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashMoneyCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpPost("Check")]
    public async Task<ActionResult<Guid>> CreateCheck([FromBody] CashCheckDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashCheckCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpPost("Cart")]
    public async Task<ActionResult<Guid>> CreateCart([FromBody] CashCartDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashCartCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("patientcashes")]
    public async Task<ActionResult<Guid>> GetCashList([FromQuery] string receptionId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetPatientCashesQuery() { ReceptionId = Guid.Parse(receptionId), OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpDelete("cash")]
    public async Task<IActionResult> RemoveCash(Guid cashtypeid, [FromQuery] CashType cashtype)
    {
        var response = await _mediator.Send(new DeleteCashCommand() { CashTypeId = cashtypeid, Cashtype = cashtype });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("total-debt")]
    public async Task<ActionResult<Guid>> GettotalDebtofreception([FromQuery] Guid officeId, [FromQuery] Guid receptionId, [FromQuery] Guid patientId)
    {
        var response = await _mediator.Send(new GetTotalDebtofReceptionQuery() { OfficeId = officeId, ReceptionId = receptionId, PatientId = patientId });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("total-received")]
    public async Task<ActionResult<CashTotalReceivedDto>> GetTotalReceived([FromQuery] Guid officeId, [FromQuery] Guid receptionId)
    {
        var response = await _mediator.Send(new GetReceptionCashTotalsQuery() { OfficeId = officeId, ReceptionId = receptionId });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpPost("returncash")]
    public async Task<ActionResult<Guid>> ReturnCash([FromQuery] string officeId, [FromQuery] string cashId)
    {
        var response = await _mediator.Send(new ReturnCashCommand() { OfficeId = Guid.Parse(officeId), CashId = Guid.Parse(cashId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpPost("CashDebt")]
    public async Task<ActionResult<Guid>> CreateCashDebt([FromBody] CashesDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashDebtCommand() { DTO = dto });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}