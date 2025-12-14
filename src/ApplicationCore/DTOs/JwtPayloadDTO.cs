using System.Collections.Generic;

namespace ApplicationCore.DTOs;

public sealed class JwtPayloadDTO
{
    public string NameId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();

    public long NotBefore { get; set; }
    public long Expires { get; set; }
    public long IssuedAt { get; set; }
}