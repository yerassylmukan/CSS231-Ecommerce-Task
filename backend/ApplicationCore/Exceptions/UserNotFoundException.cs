namespace ApplicationCore.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string userName) : base($"No user found with: {userName}")
    {
    }
    
    public UserNotFoundException() : base($"No user found")
    {
    }
}