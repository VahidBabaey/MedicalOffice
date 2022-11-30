using MediatR;
using MedicalOffice.Application.Dtos.Reception;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Requests.Commands
{
    public class AddReceptionCommand : IRequest<BaseResponse>
    {
        public ReceptionDTO DTO { get; set; } = new ReceptionDTO();
    }
}
