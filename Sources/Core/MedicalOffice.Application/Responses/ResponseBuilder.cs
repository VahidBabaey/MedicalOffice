using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Responses
{
    public static class ResponseBuilder
    {
        public static BaseResponse Success(HttpStatusCode statusCode, string message, params object[] data)
        {
            return new() { StatusCode = statusCode, Success = true, StatusDescription = message, Data = data.ToList() };
        }

        public static BaseResponse Faild(HttpStatusCode statusCode, string message, params string[] errors)
        {
            return new() { StatusCode = statusCode, Success = false, StatusDescription = message, Errors = errors.ToList() };
        }

        public static string HandlerMessage<T>(string resType)
        {
            if (nameof(T).Contains("Query"))
                return string.Concat(nameof(T).Replace("QueryHandler", string.Empty), resType);

            if (nameof(T).Contains("Command"))
                return string.Concat(nameof(T).Replace("CommandHandler", string.Empty), resType);

            else
                return string.Empty;
        }
    }
}
