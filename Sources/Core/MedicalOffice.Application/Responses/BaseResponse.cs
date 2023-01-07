using System.Net;

namespace MedicalOffice.Application.Responses;

public class BaseResponse
{
    public BaseResponse() { }

    public BaseResponse(
        HttpStatusCode statusCode,
        bool success,
        string message,
        List<string> errors,
        List<object> data)
    {
        StatusCode = statusCode;
        Success = success;
        StatusDescription = message;
        Errors = errors;
        Data = data;
    }

    public bool Success { get; set; } = true;
    public HttpStatusCode StatusCode { get; set; }
    public string StatusDescription { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new List<string>();
    public object Data { get; set; } = new object();
}

//TODO:Error type should change from "string" to "ResponseError"
public class ResponseError
{
    public int ErrorCode { get; set; }
    public string Description { get; set; } = string.Empty;
}

//public class ResponseBuilder
//{
//    public BaseResponse Success(HttpStatusCode statusCode, string message, params object[] data)
//    {
//        return new() { StatusCode = statusCode, Success = true, StatusDescription = message, Data = data.ToList() };
//    }

//    public BaseResponse Faild(HttpStatusCode statusCode, string message, params string?[] errors)
//    {
//        return new() { StatusCode = statusCode, Success = false, StatusDescription = message, Errors = errors.ToList() };
//    }
//}