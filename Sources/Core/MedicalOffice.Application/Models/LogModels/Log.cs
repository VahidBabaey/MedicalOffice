namespace MedicalOffice.Application.Models.Logger;

public class Log
{
    public Log(string header, LogType type, object messages)
    {
        Header = header;
        Type = type;
        AdditionalData = messages;
    }
    public Log()
    {
    }

    public string Header { get; set; } = string.Empty;
    public object AdditionalData { get; set; } = new object();
    public LogType Type { get; set; }
}

public enum LogType
{
    Error = 0,
    Success = 1,
}
