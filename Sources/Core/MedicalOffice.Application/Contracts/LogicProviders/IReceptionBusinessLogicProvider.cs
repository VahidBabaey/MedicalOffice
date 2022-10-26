using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.LogicProviders
{
    public interface IReceptionBusinessLogicProvider
    {
        decimal ApplyReportingFilter(decimal input);
    }
}
