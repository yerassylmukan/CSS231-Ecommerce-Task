namespace ApplicationCore.Common.Contracts;

public interface IEmailSender
{
    Task EmailSendAsync(string email, string subject, string message, CancellationToken cancellationToken);
}