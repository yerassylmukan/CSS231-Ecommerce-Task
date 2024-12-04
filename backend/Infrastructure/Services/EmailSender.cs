using System.Net;
using System.Net.Mail;
using ApplicationCore.Interfaces;

namespace Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromAddress;

    public EmailSender()
    {
        _fromAddress = "230107009@sdu.edu.com";
        _smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("ca19a9b422b017", "701a17f3157fcc"),
            EnableSsl = true
        };
    }

    public async Task EmailSendAsync(string recipientEmail, string subject, string message, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(recipientEmail))
            throw new ArgumentException("Recipient email cannot be null or empty.", nameof(recipientEmail));

        var mailMessage = new MailMessage(_fromAddress, recipientEmail, subject, message)
        {
            IsBodyHtml = true
        };

        try
        {
            var sendEmailTask = _smtpClient.SendMailAsync(mailMessage, cancellationToken);
            await Task.WhenAny(sendEmailTask, Task.Delay(Timeout.Infinite, cancellationToken));

            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("Email sending was canceled.");

            await sendEmailTask;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw new Exception("Failed to send email. See inner exception for details.", ex);
        }
    }
}