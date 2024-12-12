namespace ApplicationCore.Exceptions;

public class RoleDoesNotExistsException : Exception
{
    public RoleDoesNotExistsException(string role) : base($"Role {role} does not exist.")
    {
    }
}