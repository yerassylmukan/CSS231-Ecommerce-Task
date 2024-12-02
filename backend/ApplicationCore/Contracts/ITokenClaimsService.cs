namespace ApplicationCore.Contracts;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}