using MediatR;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.UserFile.Request.Commands
{
    public class DeleteUserCommand : IRequest<BaseCommandResponse>
    {
        public Guid UserId { get; set; }
    }
}
