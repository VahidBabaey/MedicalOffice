using MediatR;
using MedicalOffice.Application.Dtos.Service;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Commands
{
    public class EditServiceCommand : IRequest<BaseCommandResponse>
    {
        public ServiceDTO DTO { get; set; } = new ServiceDTO();
    }
}
