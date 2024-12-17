namespace ApplicationCore.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base("Invalid password")
    {
    }
}