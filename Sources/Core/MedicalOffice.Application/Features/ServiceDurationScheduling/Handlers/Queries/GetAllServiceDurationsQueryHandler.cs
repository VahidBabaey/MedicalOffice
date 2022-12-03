using MediatR;
using MedicalOffice.Application.Responses;
using MedicalOffice.WebApi.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceDurationScheduling.Handlers.Queries
{
    public class GetAllServiceDurationsQueryHandler : IRequestHandler<GetAllServiceDurationQuery, BaseResponse>
    {
        public Task<BaseResponse> Handle(GetAllServiceDurationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
