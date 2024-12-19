using System.Net;
using System.Net.Mail;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly string _fromAddress;
    private readonly SmtpClient _smtpClient;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmailSender(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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

    public async Task SendSupportAsync(string firstName, string lastName, string subject, string message,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(subject) ||
            string.IsNullOrEmpty(message))
            throw new ArgumentException("Cannot be null or empty.");

        var subjectMessage = $"Message from: {firstName} {lastName}. Subject: {subject}";

        var mailMessage = new MailMessage("support@sdu.edu.kz", "230107009@sdu.edu.kz", subjectMessage, message)
        {
            IsBodyHtml = true
        };

        var sendEmailTask = _smtpClient.SendMailAsync(mailMessage, cancellationToken);
        await Task.WhenAny(sendEmailTask, Task.Delay(Timeout.Infinite, cancellationToken));

        await sendEmailTask;
    }

    public async Task EmailSendByUserIdAsync(string userId, string subject, string message,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("Recipient email cannot be null or empty.", nameof(userId));

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user == null) return;

        var mailMessage = new MailMessage(_fromAddress, user.Email, subject, message)
        {
            IsBodyHtml = true
        };

        var sendEmailTask = _smtpClient.SendMailAsync(mailMessage, cancellationToken);
        await Task.WhenAny(sendEmailTask, Task.Delay(Timeout.Infinite, cancellationToken));

        await sendEmailTask;
    }
}