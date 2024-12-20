﻿using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class DiscountProduct : BaseEntity
    {
        public DiscountProduct(Guid discountId, Guid productStockId, double discountNumber)
        {
            DiscountId = discountId;
            ProductStockId = productStockId;
            DiscountNumber = discountNumber;
        }
        public Guid DiscountId { get; private set; }
        public Guid ProductStockId { get; private set; }
        public double DiscountNumber { get; private set; }
        [SwaggerIgnore]
        public ProductStock? ProductStock { get; private set; }
        [SwaggerIgnore]
        public Discount? Discount { get; private set; }
    }
}
