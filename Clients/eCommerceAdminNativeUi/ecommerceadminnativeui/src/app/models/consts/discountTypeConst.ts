import { TranslateService } from '@ngx-translate/core';

export class DiscountType {
  static ProductPercentDiscount: { id: string; nameKey: string } = {
    id: '6f9619ff-8b86-d011-b42d-00c04fc964ff',
    nameKey: 'ProductPercentDiscount',
  };

  static ProductCurrencyDiscount: { id: string; nameKey: string } = {
    id: 'a920bc9e-58d7-48ca-86e8-d71fa4e71764',
    nameKey: 'ProductCurrencyDiscount',
  };

  static CategoryCurrencyDiscount: { id: string; nameKey: string } = {
    id: 'fd2830d7-8d75-4616-94a2-d2dc28975a5d',
    nameKey: 'CategoryCurrencyDiscount',
  };

  static CategoryPercentDiscount: { id: string; nameKey: string } = {
    id: '055790b5-dcd6-4c94-824e-49cba24f6ff8',
    nameKey: 'CategoryPercentDiscount',
  };

  static getAllWithTranslations(
    translateService: TranslateService
  ): Array<{ id: string; name: string }> {
    const discountArray = [
      this.ProductPercentDiscount,
      this.ProductCurrencyDiscount,
      this.CategoryCurrencyDiscount,
      this.CategoryPercentDiscount,
    ];

    return discountArray.map((discount) => ({
      id: discount.id,
      name: translateService.instant(discount.nameKey),
    }));
  }
  static getAllDiscountListOfProducts(
    translateService: TranslateService
  ): Array<{ id: string; name: string }> {
    const discountArray = [
      this.ProductPercentDiscount,
      this.ProductCurrencyDiscount,
    ];

    return discountArray.map((discount) => ({
      id: discount.id,
      name: translateService.instant(discount.nameKey),
    }));
  }

  static getAllNonProductDiscounts(
    translateService: TranslateService
  ): Array<{ id: string; name: string }> {
    const discountArray = [
      this.CategoryCurrencyDiscount,
      this.CategoryPercentDiscount,
    ];

    return discountArray.map((discount) => ({
      id: discount.id,
      name: translateService.instant(discount.nameKey),
    }));
  }
}
