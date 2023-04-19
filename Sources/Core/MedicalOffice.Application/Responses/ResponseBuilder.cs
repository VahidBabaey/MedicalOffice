using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public static string FailedMessage(string name)
        {
            return HandlerMessage(name, "failed");
        }

        public static string SuccessMessage(string name)
        {
            return HandlerMessage(name, "succeeded");
        }

        private static string HandlerMessage(string name, string resType)
        {
            if (name.Contains("Query"))
                return string.Join(name.Replace("QueryHandler", string.Empty), " ", resType);

            if (name.Contains("Command"))
                return string.Join(name.Replace("CommandHandler", string.Empty), " ", resType);

            else
                return string.Empty;
        }
    }
}
