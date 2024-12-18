using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class UpdateCatalogItemPictureUrlModel
{
    [Required] public string PictureUrl { get; set; }
}