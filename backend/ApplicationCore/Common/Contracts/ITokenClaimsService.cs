namespace ApplicationCore.Common.Contracts;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}