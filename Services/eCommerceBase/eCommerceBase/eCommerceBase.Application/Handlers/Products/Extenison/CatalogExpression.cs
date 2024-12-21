using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using System.Linq.Expressions;

namespace eCommerceBase.Application.Handlers.Products.Extenison
{
    public static class CatalogExpression
    {
        public static Expression<Func<Product, bool>> SpecificationExpression(List<CatalogFilter> data)
        {
            var filterParameter = Expression.Parameter(typeof(Product), "x");
            Expression filterFinalExpression = Expression.Constant(true);

            foreach (var specItem in data.GroupBy(x => x.SpecificationAttributeId))
            {
                Expression filterExpression = Expression.Constant(false);
                var productSpecListProperty = Expression.Property(filterParameter, "ProductSpecificationList");

                foreach (var specOptionItem in specItem)
                {
                    // ProductSpecification üzerindeki SpecificationAttributeOptionId özelliğine erişiyoruz
                    var specificationParameter = Expression.Parameter(typeof(ProductSpecification), "spec");
                    var specificationProperty = Expression.Property(specificationParameter, "SpecificationAttributeOptionId");

                    var constant = Expression.Constant(specOptionItem.SpecificationAttributeOptionId);

                    // SpecificationAttributeOptionId değerini karşılaştırıyoruz
                    var specificationEquality = Expression.Equal(specificationProperty, constant);

                    // Expression.Call kullanırken doğru parametrelerle çağırıyoruz
                    var anyMethod = typeof(Enumerable)
                        .GetMethods()
                        .Where(m => m.Name == "Any" && m.GetParameters().Length == 2)
                        .Single()
                        .MakeGenericMethod(typeof(ProductSpecification));

                    // Lambda ifadesi oluşturuluyor
                    var lambda = Expression.Lambda(specificationEquality, specificationParameter);

                    var anyExpression = Expression.Call(
                        null, // statik metod olduğu için 'null' kullanılıyor
                        anyMethod,
                        productSpecListProperty,
                        lambda
                    );
                    filterExpression = Expression.Or(filterExpression, anyExpression);
                }
                filterFinalExpression = Expression.And(filterFinalExpression, filterExpression);
            }
            return Expression.Lambda<Func<Product, bool>>(filterFinalExpression, filterParameter);
        }
    }
}
