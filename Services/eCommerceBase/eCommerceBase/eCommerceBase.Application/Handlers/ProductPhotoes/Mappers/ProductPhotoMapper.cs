using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.ProductPhotoes.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ProductPhotoMapper
    {
        public static partial ProductPhoto CreateProductPhotoCommandToProductPhoto(CreateProductPhotoCommand productPhoto);
        public static partial ProductPhoto UpdateProductPhotoCommandToProductPhoto(UpdateProductPhotoCommand productPhoto);
    }
}