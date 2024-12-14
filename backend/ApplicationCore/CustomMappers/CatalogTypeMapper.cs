using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.CustomMappers;

public static class CatalogTypeMapper
{
    public static CatalogTypeDTO MapToDTO(this CatalogType catalogType)
    {
        return new CatalogTypeDTO
        {
            Id = catalogType.Id,
            Type = catalogType.Type
        };
    }
}