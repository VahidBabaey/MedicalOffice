using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.LogicProviders
{
    public static class Helper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var prop = propertyLambda.Body as MemberExpression;

            if (prop == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return prop.Member.Name;
        }
    }
}
