using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateReviewModel
{
    [Required] public string UserId { get; set; }

    [Required] public decimal Rating { get; set; }

    [Required] public string ReviewText { get; set; }

    [Required] public int CatalogItemId { get; set; }
}