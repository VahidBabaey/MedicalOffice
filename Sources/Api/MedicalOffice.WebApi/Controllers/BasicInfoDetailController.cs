using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Commands;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Queries;
using MedicalOffice.Application.Features.BasicInfoFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.WebApi.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;

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


    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] BasicInfoDetailDTO dto)
    {
        var response = await _mediator.Send(new AddBasicInfoDetailCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpGet]
    [Authorize]
    [Permission(BasicInfoPermissions.GetAllDetails)]
    public async Task<ActionResult<List<BasicInfoDetailListDTO>>> GetAll([FromQuery] ListDto dto, Guid basicinfoId, string officeId)
    {
        var response = await _mediator.Send(new GetAllBasicInfoDetailQuery() { DTO = dto, BasicInfoId = basicinfoId });

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteBasicInfoDetailCommand() { BasicInfoDetailId = id });

        return Ok(response);
    }

    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateBasicInfoDetailDTO dto)
    {
        var response = await _mediator.Send(new EditBasicInfoDetailCommand() { DTO = dto });

        return Ok(response);
    }
}