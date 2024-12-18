using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateReviewModel
{
    [Required] public string? UserId { get; set; }
    public decimal Rating { get; set; }
    public string? ReviewText { get; set; }
}