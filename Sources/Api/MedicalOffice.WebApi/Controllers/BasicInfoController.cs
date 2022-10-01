using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Membership;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Features.BasicInfoFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasicInfoController : Controller
{
    private readonly IMediator _mediator;

    public BasicInfoController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<ActionResult<List<BasicInfoListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllBasicInfoQuery() { DTO = dto });

        return Ok(response);
    }
}