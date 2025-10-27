using ConectaCompany.Infra.ExternalServices.Interfaces;
using ConectaCompany.Infra.ExternalServices.Services;

namespace ConectaCompany.Api.Setup;

public static class SmtpConfig
{
    public static void AddSmtpConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEmailService>(sp => 
            new EmailService(
                configuration["Email:SmtpHost"] ?? throw new Exception("SMTP Host not found"),
                int.Parse(configuration["Email:SmtpPort"] ?? throw new Exception("SMTP Port not found")),
                configuration["Email:SmtpEmail"] ?? throw new Exception("SMTP User not found"),
                configuration["Email:SmtpPass"] ?? throw new Exception("SMTP Pass not found")
            )
        );
    }
}