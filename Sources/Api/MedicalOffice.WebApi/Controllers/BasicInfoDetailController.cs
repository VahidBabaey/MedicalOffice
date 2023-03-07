using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Commands;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Queries;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasicInfoDetailController : Controller
{
    private readonly IMediator _mediator;

    public BasicInfoDetailController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] BasicInfoDetailDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddBasicInfoDetailCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpGet]
    public async Task<ActionResult<List<BasicInfoDetailListDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string basicinfoId, [FromQuery] string officerId , [FromQuery] Order? order)
    {
        var response = await _mediator.Send(new GetAllBasicInfoDetailQuery() { DTO = dto, BasicInfoId = Guid.Parse(basicinfoId), OfficeId = Guid.Parse(officerId), Order = order });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpDelete]
    public async Task<IActionResult> Remove(Guid Id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteBasicInfoDetailCommand() { BasicInfoDetailId = Id , OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //[Authorize]
    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateBasicInfoDetailDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditBasicInfoDetailCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }
}