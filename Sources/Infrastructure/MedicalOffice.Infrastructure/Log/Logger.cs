using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Models;
using Microsoft.Extensions.Options;

namespace MedicalOffice.WebApi.Log;

public class Logger : ILogger
{
    private readonly LoggerSettings _settings;

    public Logger(IOptions<LoggerSettings> settings)
    {
       
       // _settings = settings.Value;
    }

    public async Task Log(Application.Models.Log log)
    {
        //var logFormat = $"{string.Format("{0:yyyy/MM/dd HH:mm}", DateTime.Now)}-{log.Type}-{log.Header}\n{new string('-', 20)}\n{string.Join("\n", log.AdditionalData)}\n{new string('-', 20)}";

        //if (_settings.OutputType == LogOutputType.File)
        //{
        //    using StreamWriter logWriter = new(_settings.FilePath, true);

        //    await logWriter.WriteLineAsync(logFormat);
        //}
        //else if (_settings.OutputType == LogOutputType.Console)
        //{
        //    Console.WriteLine(logFormat);
        //}
    }
}
