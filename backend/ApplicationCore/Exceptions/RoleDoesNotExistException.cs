namespace ApplicationCore.Exceptions;

public class RoleDoesNotExistException : Exception
{
    public RoleDoesNotExistException(string role) : base($"Role {role} does not exist.")
    {
    }
}