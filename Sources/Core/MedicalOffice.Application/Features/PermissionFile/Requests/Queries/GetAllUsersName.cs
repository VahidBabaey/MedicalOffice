using MediatR;
using MedicalOffice.Application.Dtos.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Requests.Queries
{
    public class GetAllUsersName : IRequest<List<MedicalStaffNameListDTO>>
    {
    }
}
