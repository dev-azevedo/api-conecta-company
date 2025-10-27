namespace ConectaCompany.Infra.ExternalServices.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string message);
}