﻿using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ShowCaseProduct : BaseEntity
    {
        public Guid ShowCaseId { get; private set; }
        public Guid ProductId { get; private set; }
        public ShowCaseProduct(Guid showCaseId, Guid productId)
        {
            ShowCaseId = showCaseId;
            ProductId = productId;
        }
        [SwaggerIgnore]
        public ICollection<ShowCase> ShowCaseList { get; private set; } = [];
        [SwaggerIgnore]
        public Product Product { get; private set; }
    }   
}