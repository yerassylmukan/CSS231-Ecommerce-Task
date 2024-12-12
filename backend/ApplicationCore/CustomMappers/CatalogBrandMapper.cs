using ApplicationCore.DTOs;
using ApplicationCore.Entities.CatalogAggregate;

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