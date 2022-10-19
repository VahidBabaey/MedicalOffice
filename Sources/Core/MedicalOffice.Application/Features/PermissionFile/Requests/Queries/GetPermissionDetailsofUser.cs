using MediatR;
using MedicalOffice.Application.Dtos.PermissionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Requests.Queries
{
    public class GetPermissionDetailsofUser : IRequest<List<PermissionListDTO>>
    {
        public Guid UserOfficeRoleId { get; set; }
    }
}
