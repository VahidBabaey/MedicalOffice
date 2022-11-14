using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.MedicalStaffDTO
{
    public class MedicalStaffNamesDTO : BaseDto<Guid>
    {
        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// نام نقش
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
        /// <summary>
        /// آیدی نقش
        /// </summary>
        public Guid RoleId { get; set; } 
    }
}
