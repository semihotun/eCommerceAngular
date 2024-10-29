import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () =>
      import('./screens/home/home.page').then((m) => m.HomePage),
    pathMatch: 'full',
  },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'brand-list',
    loadComponent: () =>
      import('./screens/brand/brand-list/brand-list.page').then(
        (m) => m.BrandListPage
      ),
  },
  {
    path: 'create-or-update-brand/:id',
    loadComponent: () =>
      import(
        './screens/brand/create-or-update-brand/create-or-update-brand.page'
      ).then((m) => m.CreateOrUpdateBrandPage),
  },
  {
    path: 'create-or-update-brand',
    loadComponent: () =>
      import(
        './screens/brand/create-or-update-brand/create-or-update-brand.page'
      ).then((m) => m.CreateOrUpdateBrandPage),
  },
  {
    path: 'create-or-update-specification-attribute',
    loadComponent: () =>
      import(
        './screens/specification-attribute/create-or-update-specification-attribute/create-or-update-specification-attribute.page'
      ).then((m) => m.CreateOrUpdatSpecificationAttributePage),
  },
  {
    path: 'create-or-update-specification-attribute/:id',
    loadComponent: () =>
      import(
        './screens/specification-attribute/create-or-update-specification-attribute/create-or-update-specification-attribute.page'
      ).then((m) => m.CreateOrUpdatSpecificationAttributePage),
  },
  {
    path: 'specification-attribute-list',
    loadComponent: () =>
      import(
        './screens/specification-attribute/specification-attribute-list/specification-attribute-list.page'
      ).then((m) => m.SpecificationAttributeListPage),
  },
  {
    path: 'slider-list',
    loadComponent: () =>
      import('./screens/slider/slider-list/slider-list.page').then(
        (m) => m.SliderListPage
      ),
  },
  {
    path: 'create-or-update-slider',
    loadComponent: () =>
      import(
        './screens/slider/create-or-update-slider/create-or-update-slider.page'
      ).then((m) => m.CreateOrUpdateSliderPage),
  },
  {
    path: 'create-or-update-slider/:id',
    loadComponent: () =>
      import(
        './screens/slider/create-or-update-slider/create-or-update-slider.page'
      ).then((m) => m.CreateOrUpdateSliderPage),
  },
  {
    path: 'category-list',
    loadComponent: () =>
      import('./screens/category/category-list/category-list.page').then(
        (m) => m.CategoryListPage
      ),
  },
  {
    path: 'create-or-update-category',
    loadComponent: () =>
      import(
        './screens/category/create-or-update-category/create-or-update-category.page'
      ).then((m) => m.CreateOrUpdateCategoryPage),
  },
  {
    path: 'create-or-update-page',
    loadComponent: () =>
      import(
        './screens/page/create-or-update-page/create-or-update-page.page'
      ).then((m) => m.CreateOrUpdatePagePage),
  },
  {
    path: 'create-or-update-page/:id',
    loadComponent: () =>
      import(
        './screens/page/create-or-update-page/create-or-update-page.page'
      ).then((m) => m.CreateOrUpdatePagePage),
  },
  {
    path: 'page-list',
    loadComponent: () =>
      import('./screens/page/page-list/page-list.page').then(
        (m) => m.PageListPage
      ),
  },
  {
    path: 'mail-template-list',
    loadComponent: () =>
      import(
        './screens/mail-template/mail-template-list/mail-template-list.page'
      ).then((m) => m.MailTemplateListPage),
  },
  {
    path: 'create-or-update-mail-template',
    loadComponent: () =>
      import(
        './screens/mail-template/create-or-update-mail-template/create-or-update-mail-template.page'
      ).then((m) => m.CreateOrUpdateMailTemplatePage),
  },
  {
    path: 'create-or-update-mail-template/:id',
    loadComponent: () =>
      import(
        './screens/mail-template/create-or-update-mail-template/create-or-update-mail-template.page'
      ).then((m) => m.CreateOrUpdateMailTemplatePage),
  },
  {
    path: 'product-list',
    loadComponent: () =>
      import('./screens/product/product-list/product-list.page').then(
        (m) => m.ProductListPage
      ),
  },
  {
    path: 'create-or-update-product',
    loadComponent: () =>
      import(
        './screens/product/create-or-update-product/create-or-update-product.page'
      ).then((m) => m.CreateOrUpdateProductPage),
  },
  {
    path: 'create-or-update-product/:id',
    loadComponent: () =>
      import(
        './screens/product/create-or-update-product/create-or-update-product.page'
      ).then((m) => m.CreateOrUpdateProductPage),
  },
];
