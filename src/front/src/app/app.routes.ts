import { Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path:"login",
    loadComponent: ()=> import("./features/login/login-page/login-page.component").then((m)=>m.LoginPageComponent)
  },
  {
    path: "",
    loadComponent:()=> import("./features/order/order-list-page/order-list-page.component").then((m)=>m.OrderListPageComponent),
    canActivate: [AuthGuard] 
  },
  {
    path: "create",
    loadComponent:()=> import("./features/order/order-form-page/order-form-page.component").then((m)=>m.OrderFormPageComponent),
    canActivate: [AuthGuard]
  },
  {
    path: ":id",
    loadComponent:()=> import("./features/order/order-form-page/order-form-page.component").then((m)=>m.OrderFormPageComponent),
    canActivate: [AuthGuard]
  }
];
