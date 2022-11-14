using MediatR;
using MedicalOffice.Application.Dtos.MembershipDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Requests.Queries
{
    public class GetAllMemberShipsNamesQuery : IRequest<List<MembershipNamesDTO>>
    {
    }
}
