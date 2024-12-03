using System.Net;
using System.Net.Mail;
using ApplicationCore.Common.Contracts;

namespace Infrastructure.Services;

public class EmailSender : IEmailSender
{
    public async Task EmailSendAsync(string email, string subject, string message, CancellationToken cancellationToken)
    {
        var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("ca19a9b422b017", "701a17f3157fcc"),
            EnableSsl = true
        };

        try
        {
            var sendEmailTask = client.SendMailAsync("230107009@sdu.edu.com", email, subject, message, cancellationToken);
        
            await Task.WhenAny(sendEmailTask, Task.Delay(Timeout.Infinite, cancellationToken));

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            await sendEmailTask;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw;
        }
    }

}