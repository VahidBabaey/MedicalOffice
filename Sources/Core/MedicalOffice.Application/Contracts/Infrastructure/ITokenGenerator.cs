using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface ITokenGenerator
    {
        async Task<JwtSecurityToken> GenerateToken(User user);
    }
}
