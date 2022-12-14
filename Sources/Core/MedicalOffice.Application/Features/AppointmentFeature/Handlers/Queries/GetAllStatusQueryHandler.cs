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
    public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, BaseResponse>
    {
        public async Task<BaseResponse> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();

            var appointmentTypeEnum = new List<AppointmentTypeClass>();
            foreach (int item in Enum.GetValues(typeof(AppointmentType)))
            {
                appointmentTypeEnum.Add(new AppointmentTypeClass { code = item, Type = Enum.GetName(typeof(AppointmentType),item) });
            }

            return responseBuilder.Success(HttpStatusCode.OK, "succeded", appointmentTypeEnum);
        }
    }

    internal class AppointmentTypeClass
    {
        public int code { get; set; }
        public string Type{ get; set; }
    }
}
