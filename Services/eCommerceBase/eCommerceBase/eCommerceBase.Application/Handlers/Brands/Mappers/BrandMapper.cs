using eCommerceBase.Application.Handlers.Brands.Commands;
using eCommerceBase.Application.Handlers.Brands.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class BrandMapper
    {
        public static partial Brand CreateBrandCommandToBrand(CreateBrandCommand brand);
        public static partial Brand UpdateBrandCommandToBrand(UpdateBrandCommand brand);
        public static partial GetBrandByIdDTO BrandToGetBrandByIdDTO(Brand brand);
    }
}