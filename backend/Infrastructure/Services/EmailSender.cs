using System.Net;
using System.Net.Mail;
using ApplicationCore.Interfaces;

namespace Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly string _fromAddress;
    private readonly SmtpClient _smtpClient;

    public EmailSender()
    {
        _fromAddress = "230107009@sdu.edu.com";
        _smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("12096a451ed134", "e552d1377a8b96"),
            EnableSsl = true
        };
    }

    public async Task EmailSendAsync(string recipientEmail, string subject, string message,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new OperationCanceledException("Email sending was canceled.");

        if (string.IsNullOrEmpty(recipientEmail))
            throw new ArgumentException("Recipient email cannot be null or empty.", nameof(recipientEmail));

        var mailMessage = new MailMessage(_fromAddress, recipientEmail, subject, message)
        {
            IsBodyHtml = true
        };

        var sendEmailTask = _smtpClient.SendMailAsync(mailMessage, cancellationToken);
        await Task.WhenAny(sendEmailTask, Task.Delay(Timeout.Infinite, cancellationToken));

        await sendEmailTask;
    }
}