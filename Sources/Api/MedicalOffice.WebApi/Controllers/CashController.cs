using MediatR;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.CashFile.Request.Commands;
using MedicalOffice.Application.Features.CashFile.Request.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
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

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CashesDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpPost("Pos")]
    public async Task<ActionResult<Guid>> CreatePos([FromBody] CashPosDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashPosCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }
    [HttpPost("Money")]
    public async Task<ActionResult<Guid>> CreateMoney([FromBody] CashMoneyDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashMoneyCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpPost("Check")]
    public async Task<ActionResult<Guid>> CreateCheck([FromBody] CashCheckDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashCheckCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpPost("Cart")]
    public async Task<ActionResult<Guid>> CreateCart([FromBody] CashCartDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashCartCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<Guid>> GetCashList([FromQuery] Guid receptionId)
    {
        var response = await _mediator.Send(new GetPatientCashesQuery() { ReceptionId = receptionId });

        return Ok(response);
    }

    [HttpPatch("Pos")]
    public async Task<ActionResult<Guid>> UpdatePos([FromBody] UpdateCashPosDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditCashPosCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpPatch("Check")]
    public async Task<ActionResult<Guid>> UpdateCheck([FromBody] UpdateCashCheckDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditCashCheckCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpPatch("Cart")]
    public async Task<ActionResult<Guid>> UpdateCart([FromBody] UpdateCashCartDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditCashCartCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpDelete("Pos")]
    public async Task<IActionResult> RemovePos(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteCashPosCommand() { CashPosId = id });

        return Ok(response);
    }

    [HttpDelete("Cart")]
    public async Task<IActionResult> RemoveCart(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteCashCartCommand() { CashCartId = id });

        return Ok(response);
    }

    [HttpDelete("Check")]
    public async Task<IActionResult> RemoveCheck(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteCashCheckCommand() { CashCheckId = id });

        return Ok(response);
    }

    [HttpGet("Difference")]
    public async Task<ActionResult<Guid>> GetCashDifferencWithRecieved([FromQuery] Guid receptionId)
    {
        var response = await _mediator.Send(new GetCashDefferenceWithRecievedQuery() { ReceptionId = receptionId });

        return Ok(response);
    }
    [HttpPost("returncash")]
    public async Task<ActionResult<Guid>> ReturnCash([FromQuery] string officeId, [FromQuery] string cashId)
    {
        var response = await _mediator.Send(new ReturnCashCommand() { OfficeId = Guid.Parse(officeId), CashId = Guid.Parse(cashId) });

        return Ok(response);
    }
    [HttpPost("CashDebt")]
    public async Task<ActionResult<Guid>> CreateCashDebt([FromBody] CashesDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddCashDebtCommand() { DTO = dto });

        return Ok(response);
    }

}