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
    path: 'brandlist',
    loadComponent: () =>
      import('./screens/brand/brandlist/brandlist.page').then(
        (m) => m.BrandPage
      ),
  },
  {
    path: 'createorupdatebrand/:id',
    loadComponent: () =>
      import(
        './screens/brand/createorupdatebrand/createorupdatebrand.page'
      ).then((m) => m.CreateorupdatebrandPage),
  },
  {
    path: 'createorupdatebrand',
    loadComponent: () =>
      import(
        './screens/brand/createorupdatebrand/createorupdatebrand.page'
      ).then((m) => m.CreateorupdatebrandPage),
  },
  {
    path: 'createorupdatespecificationattribute',
    loadComponent: () =>
      import(
        './screens/specificationattribute/createorupdatespecificationattribute/createorupdatespecificationattribute.page'
      ).then((m) => m.CreateorupdatespecificationattributePage),
  },
  {
    path: 'createorupdatespecificationattribute/:id',
    loadComponent: () =>
      import(
        './screens/specificationattribute/createorupdatespecificationattribute/createorupdatespecificationattribute.page'
      ).then((m) => m.CreateorupdatespecificationattributePage),
  },
  {
    path: 'specificationattributelist',
    loadComponent: () =>
      import(
        './screens/specificationattribute/specificationattributelist/specificationattributelist.page'
      ).then((m) => m.SpecificationattributelistPage),
  },
];
