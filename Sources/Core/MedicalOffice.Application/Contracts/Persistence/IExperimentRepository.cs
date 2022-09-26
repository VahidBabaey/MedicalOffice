﻿using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IExperimentRepository : IGenericRepository<ExperimentPre, Guid>
    {

    }
}
