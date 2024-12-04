namespace ApplicationCore.Exceptions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException(string email) : base($"The password provided for the user with email {email} is incorrect.")
    {
    }
}