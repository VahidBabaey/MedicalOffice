using MediatR;
using MedicalOffice.Application.Features.Appointment.Requests.Commands;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Appointment.Handlers.Commands
{
    public class TransferCommandHandler : IRequestHandler<TransferCommand, BaseResponse>
    {
        public async Task<BaseResponse> Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
