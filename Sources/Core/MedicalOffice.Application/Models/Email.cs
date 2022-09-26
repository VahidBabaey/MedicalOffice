﻿namespace MedicalOffice.Application.Models;

public class Email
{
    public Email(string to, string subject, string body)
    {
        To = to;
        Subject = subject;
        Body = body;
    }

    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
