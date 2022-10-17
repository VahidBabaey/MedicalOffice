using MedicalOffice.Application.Dtos.LoginDTO;
using MedicalOffice.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginByMobilePhone(LoginByMobilePhoneDTO request);
        Task<LoginResponseDTO> LoginByNationalCode(LoginByNationalIdDTO request);
        Task<UserExistenceResponseDTO> UserExistenceByMobilePhone(MobilePhoneExistenceRequestDTO request);
        Task<UserExistenceResponseDTO> UserExistenceByNationalCode(NationalIDExistenceRequestDTO request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
