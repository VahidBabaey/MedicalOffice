﻿using Newtonsoft.Json;
using System.Net;

namespace MedicalOffice.Application.Responses.Enveloping;

public class ApiEnvelope
{
    public HttpStatusCode StatusCode { get; set; }
    public string StatusDescription { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
    public object? Data { get; set; } = new();
}

public class Envelope
{
    private ApiEnvelope _envelope;

    public Envelope(HttpStatusCode statusCode, string statusDescription)
    {
        _envelope = new ApiEnvelope
        {
            StatusCode = statusCode,
            StatusDescription = statusDescription
        };
    }

    public Envelope PutError(params string[] errors)
    {
        _envelope.Errors = errors.ToList();

        return this;
    }

    public Envelope PutData(object data)
    {
        _envelope.Data = data;

        return this;
    }

    public ApiEnvelope Pack() => _envelope;
    public string PackJson() => JsonConvert.SerializeObject(_envelope);
}
