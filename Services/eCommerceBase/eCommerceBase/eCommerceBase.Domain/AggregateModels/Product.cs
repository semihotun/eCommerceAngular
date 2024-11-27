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
        public string? Slug { get; private set; }

        public Product(string productName, Guid? brandId, Guid? categoryId, string productContent, string gtin, string sku)
        {
            ProductName = productName;
            BrandId = brandId;
            CategoryId = categoryId;
            ProductContent = productContent;
            Gtin = gtin;
            Sku = sku;
            ProductNameUpper = productName.ToUpper();
        }

        public void SetSlug(string slug)
        {
            Slug = slug;
        }

        [SwaggerIgnore]
        public Brand? Brand { get; private set; }

        [SwaggerIgnore]
        public Category? Category { get; private set; }

        [SwaggerIgnore]
        public ICollection<ProductSpecification> ProductSpecificationList { get; private set; } = [];

        [SwaggerIgnore]
        public ICollection<ProductPhoto> ProductPhotoList { get; private set; } = [];

        [SwaggerIgnore]
        public ICollection<ShowCaseProduct> ShowCaseProductList { get; private set; } = [];

        [SwaggerIgnore]
        public ICollection<ProductStock> ProductStockList { get; private set; } = [];

        public void SetBrand(Brand? brand)
        {
            Brand = brand;
        }

        public void SetCategory(Category? category)
        {
            Category = category;
        }

        public void AddProductSpecificationList(ProductSpecification? productSpecification)
        {
            if (productSpecification != null)
            {
                ProductSpecificationList.Add(productSpecification);
            }
        }

        public void AddProductPhotoList(ProductPhoto? productPhoto)
        {
            if (productPhoto != null)
            {
                ProductPhotoList.Add(productPhoto);
            }
        }

        public void AddShowCaseProductList(ShowCaseProduct? showCaseProduct)
        {
            if (showCaseProduct != null)
            {
                ShowCaseProductList.Add(showCaseProduct);
            }
        }

        public void AddProductStockList(ProductStock? productStock)
        {
            if (productStock != null)
            {
                ProductStockList.Add(productStock);
            }
        }
    }
}