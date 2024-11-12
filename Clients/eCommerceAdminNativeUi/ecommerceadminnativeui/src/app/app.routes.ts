import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: 'home',
    loadComponent: () =>
      import('./screens/home/home.page').then((m) => m.HomePage),
    pathMatch: 'full',
    canActivate: [AuthGuard],
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
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-brand/:id',
    loadComponent: () =>
      import(
        './screens/brand/create-or-update-brand/create-or-update-brand.page'
      ).then((m) => m.CreateOrUpdateBrandPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-brand',
    loadComponent: () =>
      import(
        './screens/brand/create-or-update-brand/create-or-update-brand.page'
      ).then((m) => m.CreateOrUpdateBrandPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-specification-attribute',
    loadComponent: () =>
      import(
        './screens/specification-attribute/create-or-update-specification-attribute/create-or-update-specification-attribute.page'
      ).then((m) => m.CreateOrUpdatSpecificationAttributePage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-specification-attribute/:id',
    loadComponent: () =>
      import(
        './screens/specification-attribute/create-or-update-specification-attribute/create-or-update-specification-attribute.page'
      ).then((m) => m.CreateOrUpdatSpecificationAttributePage),
    canActivate: [AuthGuard],
  },
  {
    path: 'specification-attribute-list',
    loadComponent: () =>
      import(
        './screens/specification-attribute/specification-attribute-list/specification-attribute-list.page'
      ).then((m) => m.SpecificationAttributeListPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'slider-list',
    loadComponent: () =>
      import('./screens/slider/slider-list/slider-list.page').then(
        (m) => m.SliderListPage
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-slider',
    loadComponent: () =>
      import(
        './screens/slider/create-or-update-slider/create-or-update-slider.page'
      ).then((m) => m.CreateOrUpdateSliderPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-slider/:id',
    loadComponent: () =>
      import(
        './screens/slider/create-or-update-slider/create-or-update-slider.page'
      ).then((m) => m.CreateOrUpdateSliderPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'category-list',
    loadComponent: () =>
      import('./screens/category/category-list/category-list.page').then(
        (m) => m.CategoryListPage
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-category',
    loadComponent: () =>
      import(
        './screens/category/create-or-update-category/create-or-update-category.page'
      ).then((m) => m.CreateOrUpdateCategoryPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-page',
    loadComponent: () =>
      import(
        './screens/page/create-or-update-page/create-or-update-page.page'
      ).then((m) => m.CreateOrUpdatePagePage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-page/:id',
    loadComponent: () =>
      import(
        './screens/page/create-or-update-page/create-or-update-page.page'
      ).then((m) => m.CreateOrUpdatePagePage),
    canActivate: [AuthGuard],
  },
  {
    path: 'page-list',
    loadComponent: () =>
      import('./screens/page/page-list/page-list.page').then(
        (m) => m.PageListPage
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'mail-template-list',
    loadComponent: () =>
      import(
        './screens/mail-template/mail-template-list/mail-template-list.page'
      ).then((m) => m.MailTemplateListPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-mail-template',
    loadComponent: () =>
      import(
        './screens/mail-template/create-or-update-mail-template/create-or-update-mail-template.page'
      ).then((m) => m.CreateOrUpdateMailTemplatePage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-mail-template/:id',
    loadComponent: () =>
      import(
        './screens/mail-template/create-or-update-mail-template/create-or-update-mail-template.page'
      ).then((m) => m.CreateOrUpdateMailTemplatePage),
    canActivate: [AuthGuard],
  },
  {
    path: 'product-list',
    loadComponent: () =>
      import('./screens/product/product-list/product-list.page').then(
        (m) => m.ProductListPage
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-product',
    loadComponent: () =>
      import(
        './screens/product/create-or-update-product/create-or-update-product.page'
      ).then((m) => m.CreateOrUpdateProductPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-product/:id',
    loadComponent: () =>
      import(
        './screens/product/create-or-update-product/create-or-update-product.page'
      ).then((m) => m.CreateOrUpdateProductPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./screens/register/register.page').then((m) => m.RegisterPage),
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./screens/login/login.page').then((m) => m.LoginPage),
  },
  {
    path: 'showcase-list',
    loadComponent: () =>
      import('./screens/showcase/showcase-list/showcase-list.page').then(
        (m) => m.ShowcaseListPage
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-showcase',
    loadComponent: () =>
      import(
        './screens/showcase/create-or-update-showcase/create-or-update-showcase.page'
      ).then((m) => m.CreateOrUpdateShowcasePage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-showcase/:id',
    loadComponent: () =>
      import(
        './screens/showcase/create-or-update-showcase/create-or-update-showcase.page'
      ).then((m) => m.CreateOrUpdateShowcasePage),
    canActivate: [AuthGuard],
  },
  {
    path: 'warehouse-list',
    loadComponent: () =>
      import('./screens/warehouse/warehouse-list/warehouse-list.page').then(
        (m) => m.WarehouseListPage
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-warehouse',
    loadComponent: () =>
      import(
        './screens/warehouse/create-or-update-warehouse/create-or-update-warehouse.page'
      ).then((m) => m.CreateOrUpdateWarehousePage),
    canActivate: [AuthGuard],
  },
  {
    path: 'create-or-update-warehouse/:id',
    loadComponent: () =>
      import(
        './screens/warehouse/create-or-update-warehouse/create-or-update-warehouse.page'
      ).then((m) => m.CreateOrUpdateWarehousePage),
    canActivate: [AuthGuard],
  },
];
