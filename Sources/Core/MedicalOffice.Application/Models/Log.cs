namespace MedicalOffice.Application.Models;

public class Log
{
    public Log(string header, LogType type, List<string> messages)
    {
        Header = header;
        Type = type;
        Messages = messages;
    }
    public Log()
    {
    }

    public string Header { get; set; } = string.Empty;
    public List<string> Messages { get; set; } = new List<string>();
    public LogType Type { get; set; }
}

public enum LogType
{
    Error = 0,
    Success = 1,
}
