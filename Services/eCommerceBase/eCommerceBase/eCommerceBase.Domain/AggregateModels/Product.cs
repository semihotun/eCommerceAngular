using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class Product : BaseEntity
    {
        public string ProductName { get; private set; }
        public Guid? BrandId { get; private set; }
        public Guid? CategoryId { get; private set; }
        public string ProductContent { get; private set; }
        public string Gtin { get; private set; }
        public string Sku { get; private set; }
        public string ProductNameUpper { get; private set; }
        public string ProductSeo { get; private set; }
        public Product(string productName, Guid? brandId, Guid? categoryId,
            string productContent, string gtin, string sku)
        {
            ProductName = productName;
            BrandId = brandId;
            CategoryId = categoryId;
            ProductContent = productContent;
            Gtin = gtin;
            Sku = sku;
            ProductNameUpper = productName.ToUpper();
        }
        public void SetSlug(string productSeo)
        {
            ProductSeo = productSeo;
        }
        [SwaggerIgnore]
        public Brand? Brand { get; private set; }
        [SwaggerIgnore]
        public Category? Category { get; private set; }
        [SwaggerIgnore]
        public ICollection<ProductSpecification> ProductSpecificationList { get; private set; } = [];
        [SwaggerIgnore]
        public ICollection<ProductPhoto> ProductPhotoList { get; private set; } = [];
    }
}