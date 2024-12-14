using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.CustomMappers;

public static class CatalogBrandMapper
{
    public static CatalogBrandDTO MapToDTO(this CatalogBrand catalogBrand)
    {
        return new CatalogBrandDTO
        {
            Id = catalogBrand.Id,
            Brand = catalogBrand.Brand
        };
    }
}