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
];
