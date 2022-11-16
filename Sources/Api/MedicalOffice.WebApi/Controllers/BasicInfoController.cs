using MediatR;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.BasicInfoFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
    [Authorize]
    public async Task<ActionResult<List<BasicInfoListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        string office = User.Claims.Where(c => c.Type == "office").Select(x => x.Value).FirstOrDefault() ?? string.Empty;

        if (User.HasClaim("office", office))
        {
            var response = await _mediator.Send(new GetAllBasicInfoQuery() { DTO = dto });

            return Ok(response);
        }
        else
        {
            throw new Exception();
        }
    }
}