using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class SupportRequestModel
{
    [Required] public string FromAddress { get; set; }

    [Required] public string Subject { get; set; }

    [Required] public string Message { get; set; }
}