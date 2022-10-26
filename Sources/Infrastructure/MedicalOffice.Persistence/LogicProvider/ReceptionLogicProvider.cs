using MedicalOffice.Application.Contracts.LogicProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Persistence.LogicProvider
{
    internal class ReceptionLogicProvider : IReceptionBusinessLogicProvider
    {
        public decimal ApplyReportingFilter(decimal input)
        {
            if (input > 1000_000)
                return input;
            else
                return 0;
        }
    }
}
