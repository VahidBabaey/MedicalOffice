using MedicalOffice.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.RoleDTO
{
    public class RoleDTO
    {
        /// <summary>
        /// شناسه نقش
        /// </summary>
        public Guid Id{ get; set; }

        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نام فارسی
        /// </summary>
        public string PersianName { get; set; }
    }
}
