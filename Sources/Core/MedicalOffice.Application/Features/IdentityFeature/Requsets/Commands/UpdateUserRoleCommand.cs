using MediatR;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands
{
    public class UpdateUserRoleCommand : IRequest<BaseResponse>
    {
        public UserRoleDTO Dto{ get; set; }  
    }
}