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

        var template = _template;
        
        // Replace placeholders in the template
        template = template.Replace("{{titulo}}", subject)
                           .Replace("{{mensagem}}", body)
                           .Replace("{{ano}}", DateTime.Now.Year.ToString());
        
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpEmail, "Conecta Company"),
            Subject = subject,
            Body = template,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);

        await client.SendMailAsync(mailMessage);
    }


    private string _template = @"
        <!DOCTYPE html>
<html lang=""pt-BR"">
  <head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>ConectaCompany - Notificação</title>
  </head>
  <body style=""margin:0; padding:0; font-family:Arial, Helvetica, sans-serif; color:#4E5861;"">
    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""padding:40px 0;"">
      <tr>
        <td align=""center"">
          <table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background-color:#FFFFFF; border-radius:10px; overflow:hidden;"">
            
            <!-- Cabeçalho -->
            <tr>
              <td align=""center"" style=""background-color:#4E5861; padding:20px;"">
                <h1 style=""margin:0; color:#FFF5D6; font-size:22px; font-weight:600;"">ConectaCompany</h1>
              </td>
            </tr>

            <!-- Corpo -->
            <tr>
              <td style=""padding:30px; color:#4E5861;"">
                <h2 style=""margin-top:0; font-size:20px; color:#4E5861;"">{{titulo}} 👋</h2>
                <p style=""font-size:16px; line-height:1.6;"">
                  {{mensagem}}
                </p>

                <p style=""font-size:13px; text-align:center; color:#4E5861; opacity:0.8;"">
                  Caso não tenha solicitado esta ação, ignore este e-mail.
                </p>
              </td>
            </tr>

            <!-- Rodapé -->
            <tr>
              <td align=""center"" style=""padding:16px; font-size:12px; background-color:#FFF5D6; color:#4E5861;"">
                © <span id=""ano-footer"">{{ano}}</span> ConectaCompany. Todos os direitos reservados.
              </td>
            </tr>

          </table>
        </td>
      </tr>
    </table>`
    
    <script type=""text/javascript"" charset=""utf-8"">
      document.querySelector(""#ano-footer"").text = ""2025""
    </script>
  </body>
</html>
";
}