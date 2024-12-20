﻿using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using System.Linq.Expressions;

namespace eCommerceBase.Application.Handlers.Products.Extenison
{
    public class ProductDetailExtension
    {
        public static Expression<Func<Product, ProductDetailDTO>> ToProductDetailDTO(Guid userId)
        {
            return (Product product) => new ProductDetailDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Slug = product.Slug,
                Price = product.ProductStockList
                           .AsQueryable()
                           .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                           .OrderBy(stock => stock.CreatedOnUtc)
                           .Select(ProductQueryExtensions.ToCalculatePrice)
                           .FirstOrDefault(),
                PriceWithoutDiscount = product.ProductStockList
                            .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                            .OrderBy(stock => stock.CreatedOnUtc)
                            .First()!.Price,
                ImageUrl = product.ProductPhotoList
                            .Where(photo => !photo.Deleted)
                            .First()!.ImageUrl,
                CurrencyCode = product.ProductStockList
                            .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                            .OrderBy(stock => stock.CreatedOnUtc)
                            .First()!.Currency!.Code,
                BrandName = product.Brand!.BrandName,
                CategoryName = product.Category!.CategoryName,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                ProductContent = product.ProductContent,
                CommentCount = product.ProductCommentList.Count,
                AvgStarRate = product.ProductCommentList
                    .Where(x => x.IsApprove)
                    .Select(x => x.Rate)
                    .AsEnumerable()
                    .DefaultIfEmpty()
                    .Average(),
                FavoriteId = userId != Guid.Empty
                                        ? product.ProductFavoriteList
                                         .FirstOrDefault(x => x.CustomerUserId == userId && !x.Deleted)!.Id
                                        : null
            };
        }
    }
}
