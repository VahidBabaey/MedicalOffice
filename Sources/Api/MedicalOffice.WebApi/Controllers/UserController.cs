using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.UserDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.UserFile.Request.Commands;
using MedicalOffice.Application.Features.UserFile.Request.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;

using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] UserDTO dto)
    {
        var response = await _mediator.Send(new AddUserCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpPatch]
    public async Task<ActionResult<Guid>> UpdateUser([FromBody] UpdateUserDTO dto)
    {
        var response = await _mediator.Send(new EditUserCommand() { DTO = dto });

        return Ok(response);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteUserCommand() { UserId = id });

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<List<UserListDTO>>> GetAll([FromQuery] ListDto dto)
    {
        var response = await _mediator.Send(new GetAllUsers() { DTO = dto });

        return Ok(response);
    }
}