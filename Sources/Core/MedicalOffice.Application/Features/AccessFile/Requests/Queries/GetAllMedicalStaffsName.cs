using MediatR;
using MedicalOffice.Application.Dtos.Userdto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AccessFile.Requests.Queries
{
    public class GetAllUsersName : IRequest<List<UserNameListDTO>>
    {
    }
}
