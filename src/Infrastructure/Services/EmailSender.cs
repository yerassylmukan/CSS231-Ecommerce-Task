using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly string _fromAddress;
    private readonly string _password;
    private readonly string _host;
    private readonly int _port;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmailSender(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

        _host = Environment.GetEnvironmentVariable("SMTP_HOST")
                ?? throw new InvalidOperationException("SMTP_HOST not set");

        _port = int.Parse(
            Environment.GetEnvironmentVariable("SMTP_PORT")
            ?? throw new InvalidOperationException("SMTP_PORT not set"));

        _fromAddress = Environment.GetEnvironmentVariable("SMTP_FROM")
                       ?? throw new InvalidOperationException("SMTP_FROM not set");

        _password = Environment.GetEnvironmentVariable("SMTP_PASSWORD")
                    ?? throw new InvalidOperationException("SMTP_PASSWORD not set");
    }

    public async Task EmailSendAsync(string recipientEmail, string subject, string message,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(recipientEmail))
            throw new ArgumentException("Recipient email cannot be null or empty.", nameof(recipientEmail));

        var email = BuildEmail(_fromAddress, recipientEmail, subject, message);

        await SendAsync(email, cancellationToken);
    }

    public async Task SendSupportAsync(string firstName, string lastName, string subject, string message,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(subject) ||
            string.IsNullOrEmpty(message))
            throw new ArgumentException("Cannot be null or empty.");

        var subjectMessage = $"Message from: {firstName} {lastName}. Subject: {subject}";
        var email = BuildEmail(_fromAddress, "230107009@sdu.edu.kz", subjectMessage, message);

        await SendAsync(email, cancellationToken);
    }

    public async Task EmailSendByUserIdAsync(string userId, string subject, string message,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("Recipient email cannot be null or empty.", nameof(userId));

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user?.Email == null) return;

        var email = BuildEmail(_fromAddress, user.Email, subject, message);

        await SendAsync(email, cancellationToken);
    }

    private MimeMessage BuildEmail(string from, string to, string subject, string htmlBody)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;

        email.Body = new BodyBuilder
        {
            HtmlBody = htmlBody
        }.ToMessageBody();

        return email;
    }

    private async Task SendAsync(MimeMessage email, CancellationToken cancellationToken)
    {
        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(_host, _port, SecureSocketOptions.StartTls, cancellationToken);
        await smtp.AuthenticateAsync(_fromAddress, _password, cancellationToken);
        await smtp.SendAsync(email, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }
}
