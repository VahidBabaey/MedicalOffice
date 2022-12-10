using MediatR;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public class GetDoctorDateTimesQueryhandler : IRequestHandler<GetDoctorDateTimesQuery, BaseResponse>
    {
        public Task<BaseResponse> Handle(GetDoctorDateTimesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
