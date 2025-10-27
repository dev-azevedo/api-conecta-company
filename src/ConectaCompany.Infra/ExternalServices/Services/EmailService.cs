using System.Net;
using System.Net.Mail;
using ConectaCompany.Infra.ExternalServices.Interfaces;

namespace ConectaCompany.Infra.ExternalServices.Services;

public class EmailService : IEmailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpEmail;
    private readonly string _smtpPass;

    public EmailService(string smtpHost, int smtpPort, string smtpEmail, string smtpPass)
    {
        _smtpHost = smtpHost;
        _smtpPort = smtpPort;
        _smtpEmail = smtpEmail;
        _smtpPass = smtpPass;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpEmail, _smtpPass),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpEmail, "Conecta Company"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);

        await client.SendMailAsync(mailMessage);
    }
}