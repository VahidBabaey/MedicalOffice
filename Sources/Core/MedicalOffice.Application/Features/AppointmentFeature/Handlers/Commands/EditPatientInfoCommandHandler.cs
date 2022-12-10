using MediatR;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Commands
{
    public class EditAppointmentPatientCommandHandler : IRequestHandler<EditAppointmentPatientCommand, BaseResponse>
    {
        public Task<BaseResponse> Handle(EditAppointmentPatientCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
