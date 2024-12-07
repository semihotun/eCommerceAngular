import { Routes } from '@angular/router';
import { LoginPage } from './screens/login/login.page';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./screens/home/home.page').then((m) => m.HomePage),
    pathMatch: 'full',
  },
  {
    path: '',
    redirectTo: '',
    pathMatch: 'full',
  },
  {
    path: 'basket',
    loadComponent: () =>
      import('./screens/basket/basket.page').then((m) => m.BasketPage),
    pathMatch: 'full',
  },
  {
    path: 'catalog',
    loadComponent: () =>
      import('./screens/catalog/catalog.page').then((m) => m.CatalogPage),
    pathMatch: 'full',
  },
  {
    path: 'comments',
    loadComponent: () =>
      import('./screens/comments/comments.page').then((m) => m.CommentsPage),
    pathMatch: 'full',
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./screens/login/login.page').then((m) => m.LoginPage),
    pathMatch: 'full',
  },
  {
    path: 'login/:activationCode',
    loadComponent: () =>
      import('./screens/login/login.page').then((m) => m.LoginPage),
    pathMatch: 'full',
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./screens/register/register.page').then((m) => m.RegisterPage),
    pathMatch: 'full',
  },
  {
    path: 'user-management',
    loadComponent: () =>
      import('./screens/user-management/user-management.page').then(
        (m) => m.UserManagementPage
      ),
    canActivate: [AuthGuard],
  },
  {
    path: ':slug/mobile-product-comments',
    loadComponent: () =>
      import(
        './screens/product-detail/components/product-tabs/components/mobile-product-tabs/components/mobile-product-comments/mobile-product-comments.page'
      ).then((m) => m.MobileProductCommentsPage),
    pathMatch: 'full',
  },
  {
    path: ':slug/mobile-product-question-answer',
    loadComponent: () =>
      import(
        './screens/product-detail/components/product-tabs/components/mobile-product-tabs/components/mobile-product-question-answer/mobile-product-question-answer.page'
      ).then((m) => m.MobileProductQuestionAnswerPage),
    pathMatch: 'full',
  },
  {
    path: ':slug/product-specification',
    loadComponent: () =>
      import(
        './screens/product-detail/components/product-tabs/components/mobile-product-tabs/components/mobile-product-specification/mobile-product-specification.page'
      ).then((m) => m.MobileProductSpecificationPage),
    pathMatch: 'full',
  },
  {
    path: 'activation-code',
    loadComponent: () =>
      import('./screens/activation-code/activation-code.page').then(
        (m) => m.ActivationCodePage
      ),
  },
  {
    path: 'activation-code/:id',
    loadComponent: () =>
      import('./screens/activation-code/activation-code.page').then(
        (m) => m.ActivationCodePage
      ),
  },
  {
    path: 'user-info',
    loadComponent: () =>
      import('./screens/user-info/user-info.page').then((m) => m.UserInfoPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'favorites',
    loadComponent: () =>
      import('./screens/favorites/favorites.page').then((m) => m.FavoritesPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'addresses',
    loadComponent: () =>
      import('./screens/addresses/addresses.page').then((m) => m.AddressesPage),
    canActivate: [AuthGuard],
  },
  {
    path: 'orders',
    loadComponent: () =>
      import('./screens/orders/orders.page').then((m) => m.OrdersPage),
    canActivate: [AuthGuard],
  },
  {
    path: ':slug',
    loadComponent: () =>
      import('./screens/product-detail/product-detail.page').then(
        (m) => m.ProductDetailPage
      ),
    pathMatch: 'full',
  },
];
