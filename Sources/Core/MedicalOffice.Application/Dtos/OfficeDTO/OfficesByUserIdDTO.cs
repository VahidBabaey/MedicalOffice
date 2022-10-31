using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.OfficeDTO
{
    public class OfficesByUserIdDTO : ListDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;  
    }
}
