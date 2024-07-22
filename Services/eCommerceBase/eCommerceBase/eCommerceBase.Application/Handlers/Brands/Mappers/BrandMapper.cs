using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.Brands.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class BrandMapper
    {
        public static partial Brand CreateBrandCommandToBrand(CreateBrandCommand brand);
        public static partial void UpdateBrandCommandToBrand(UpdateBrandCommand updateBrandCommand, Brand brand);
    }
}