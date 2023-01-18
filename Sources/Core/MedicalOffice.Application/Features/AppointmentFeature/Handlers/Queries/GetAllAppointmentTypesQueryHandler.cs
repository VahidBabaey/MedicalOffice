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
    public class GetAllAppointmentTypesQueryHandler : IRequestHandler<GetAllStatusQuery, BaseResponse>
    {
        public Task<BaseResponse> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
            
            var appointmentTypes = Enum.GetValues<AppointmentType>().ToList().Select(a => new 
            {
                Key = a.ToString(),
                Value = Convert.ToInt32(a)
            });

            return Task.FromResult(ResponseBuilder.Success(HttpStatusCode.OK, "succeded", appointmentTypes));
        }
    }
}
