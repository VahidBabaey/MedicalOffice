using MediatR;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands
{
    public class SendTotpCommand : IRequest<BaseResponse>
    {
        public PhoneNumberDTO DTO { get; set; } = new PhoneNumberDTO();
    }
}
