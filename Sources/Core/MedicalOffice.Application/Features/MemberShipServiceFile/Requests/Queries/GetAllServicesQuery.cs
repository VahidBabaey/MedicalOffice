using MediatR;
using MedicalOffice.Application.Dtos.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Queries
{
    public class GetAllServicesQuery : IRequest<List<ServiceListDTO>>
    {

    }
}
