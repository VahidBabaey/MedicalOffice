using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.validationHelper
{
    public static class PropertyHelper
    {
        public static string GetName<T>(Expression<Func<T, object>> property)
        {
            LambdaExpression lambda = property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression unaryExpression)
                memberExpression = (MemberExpression)(unaryExpression.Operand);
            else
                memberExpression = (MemberExpression)(lambda.Body);

            return ((PropertyInfo)memberExpression.Member).Name;
        }
    }
}
