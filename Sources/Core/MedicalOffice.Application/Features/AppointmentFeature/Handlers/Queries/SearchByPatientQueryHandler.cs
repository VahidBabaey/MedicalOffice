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
    public class SearchByPatientQueryHandler : IRequestHandler<SearchByPatientQuery, BaseResponse>
    {
        public async Task<BaseResponse> Handle(SearchByPatientQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
