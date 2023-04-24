﻿namespace MedicalOffice.Application.Models.Logger;

public class LoggerSettings
{
    public LoggerSettings()
    {

    }
    public LoggerSettings(LogOutputType outputType, string filePath)
    {
        OutputType = outputType;
        FilePath = filePath;
    }

    public LogOutputType OutputType { get; set; }
    public string FilePath { get; set; } = string.Empty;
}

public enum LogOutputType
{
    File,
    Console,
    All
}
