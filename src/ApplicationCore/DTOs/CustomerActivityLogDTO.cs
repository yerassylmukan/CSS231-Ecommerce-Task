namespace ApplicationCore.DTOs;

public class CustomerActivityLogDTO
{
    public string UserId { get; set; }
    public bool JustOrdered { get; set; }
    public DateTimeOffset OrderDate { get; set; }
}