using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class SupportRequestModel
{
    [Required] public string FistName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Subject { get; set; }
    [Required] public string Message { get; set; }
}