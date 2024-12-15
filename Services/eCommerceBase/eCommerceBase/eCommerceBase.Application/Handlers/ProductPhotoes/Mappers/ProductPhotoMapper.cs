using eCommerceBase.Application.Handlers.ProductPhotoes.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ProductPhotoMapper
    {
        public static partial ProductPhoto CreateProductPhotoCommandToProductPhoto(CreateProductPhotoCommand productPhoto);
        public static partial ProductPhoto UpdateProductPhotoCommandToProductPhoto(UpdateProductPhotoCommand productPhoto);
    }
}