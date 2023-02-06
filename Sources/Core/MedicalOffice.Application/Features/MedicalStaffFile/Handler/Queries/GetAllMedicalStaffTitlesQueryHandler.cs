using MediatR;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public class GetAllMedicalStaffTitlesQueryHandler : IRequestHandler<GetAllMedicalStaffQueryHandler, BaseResponse>
    {
        public Task<BaseResponse> Handle(GetAllMedicalStaffQueryHandler request, CancellationToken cancellationToken)
        {
            
            var medicalStaffTiltles = Enum.GetValues<Title>().ToList().Select(a => new 
            {
                Key = a.ToString(),
                Value = Convert.ToInt32(a)
            });

            return Task.FromResult(ResponseBuilder.Success(HttpStatusCode.OK, "succeded", medicalStaffTiltles));
        }
    }
}
