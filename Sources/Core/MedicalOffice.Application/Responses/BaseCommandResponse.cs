namespace MedicalOffice.Application.Responses;

public class BaseCommandResponse
{
    public BaseCommandResponse() { }

    public BaseCommandResponse(bool success, string message, List<string> errors, List<object> data)
    {
        Success = success;
        Message = message;
        Errors = errors;
        Data = data;
    }

    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new List<string>();
    public List<object> Data { get; set; } = new List<object>();
}