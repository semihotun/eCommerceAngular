using System.ComponentModel.DataAnnotations;

namespace eCommerceBase.Domain.Constant
{
    public class DiscountTypeConst
    {
        [Display(Name = "ProductPercentDiscount")]
        public static Guid ProductPercentDiscount = Guid.Parse("6f9619ff-8b86-d011-b42d-00c04fc964ff");
        [Display(Name = "ProductCurrencyDiscount")]
        public static Guid ProductCurrencyDiscount = Guid.Parse("a920bc9e-58d7-48ca-86e8-d71fa4e71764");
        [Display(Name = "CategoryCurrencyDiscount")]
        public static Guid CategoryCurrencyDiscount = Guid.Parse("fd2830d7-8d75-4616-94a2-d2dc28975a5d");
        [Display(Name = "CategoryPercentDiscount")]
        public static Guid CategoryPercentDiscount = Guid.Parse("055790b5-dcd6-4c94-824e-49cba24f6ff8");
    }
}
