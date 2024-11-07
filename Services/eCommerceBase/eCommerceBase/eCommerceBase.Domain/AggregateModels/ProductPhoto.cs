using eCommerceBase.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductPhoto : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public string PhotoBase64 { get; private set; }

        public ProductPhoto(Guid productId, string photoBase64)
        {
            ProductId = productId;
            PhotoBase64 = photoBase64;
        }

        public Product? Product { get; private set; }

        public void SetProduct(Product? product)
        {
            Product = product;
        }
    }
}