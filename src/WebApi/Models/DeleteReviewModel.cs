using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class DeleteReviewModel
{
    [Required] public string UserId { get; set; }
}