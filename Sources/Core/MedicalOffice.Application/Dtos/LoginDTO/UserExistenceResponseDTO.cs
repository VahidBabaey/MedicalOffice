using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.LoginDTO
{
    public class UserExistenceResponseDTO
    {
        public bool IsExist{ get; set; }
        public string? Name { get; set; }
        public string? LastName{ get; set; }
    }
}
