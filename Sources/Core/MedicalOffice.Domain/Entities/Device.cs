﻿using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Domain
{
    public class Device : BaseDomainEntity<Guid>
    {
        public string Name { get; set; }

        public Guid? MedicalStaffId { get; set; }

        public MedicalStaff MedicalStaff { get; set; }

        public Guid RoomId { get; set; }

        public Room Room { get; set; }
    }
}