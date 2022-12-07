using MediatR;
using MedicalOffice.Application.Features.Appointment.Requests.Queries;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Appointment.Handlers.Queries
{
    public class SearchByRequestedFieldsQueryHandler : IRequestHandler<SearchByRequestedFieldsQuery, BaseResponse>
    {
        public Task<BaseResponse> Handle(SearchByRequestedFieldsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
