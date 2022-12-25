using MedicalOffice.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Constants;

public enum ValidationErrorCode
{
    Required = 100,
    MaximumLength = 101,
    MinimumLength = 102,
    NotValid = 103,
    GreaterThan = 104,
    GreaterOrEqual = 105,
    LessThan = 106,
    LessOrEqual = 107,
    TimeOnlyPattern = 108
}
public class ValidationError
{
    public ValidationErrorCode ErrorCode { get; }

    public string Message { get; }

    public ValidationError(string message, ValidationErrorCode errorCode)
    {
        Message = message;
        ErrorCode = errorCode;
    }
}
public static class ValidationMessage
{
    public static readonly ValidationError Required = new("{0} is required", ValidationErrorCode.Required);

    public static readonly ValidationError MaximumLength = new("Maximum length of {0} should be {1}", ValidationErrorCode.MaximumLength);

    public static readonly ValidationError MinimumLength = new("Minimum length of {0} should be {1}", ValidationErrorCode.MinimumLength);

    public static readonly ValidationError NotValid = new("{0} is not valid",ValidationErrorCode.NotValid);

    public static readonly ValidationError GreaterThan = new("{0} should be Greater than {1}", ValidationErrorCode.GreaterThan);

    public static readonly ValidationError GreaterOrEqual = new("{0} should be greater than or equal {1}", ValidationErrorCode.GreaterOrEqual);

    public static readonly ValidationError LessThan = new("{0} should be Less than {1}", ValidationErrorCode.LessThan);

    public static readonly ValidationError LessOrEqual = new("{0} should be less than or equal {1}", ValidationErrorCode.LessOrEqual);

    public static readonly ValidationError TimeOnlyPattern = new("{0} should match with HH:MM pattern", ValidationErrorCode.TimeOnlyPattern);
}
public static class ValidationErrorExtensions
{
    public static string For(this ValidationError error, params object?[]? inputs)
    {
        if (inputs != null)
            return string.Format(error.Message, inputs);
        else
            return string.Empty;
    }

    //public static string For<T>(this ValidationError error, params Expression<Func<T, object>>[] expressions)
    //{
    //    var propertyNames = expressions.Select(exp => exp.GetPropertyAccess().Name).ToArray();
    //    var result = string.Format(error.Message, propertyNames);

    //    return result;
    //}
}
