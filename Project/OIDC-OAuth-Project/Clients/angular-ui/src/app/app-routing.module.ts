import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { DiariesIndexComponent } from './diaries-index/diaries-index.component';
import { ViewDiariesComponent } from './view-diaries/view-diaries.component';
import { AuthGuard } from './auth/auth.guard';
import { AuthCallbackComponent } from './auth/auth-callback/auth-callback.component';

export const routes: Routes = [
  { path: '', redirectTo: 'diaries/view', pathMatch:'full' },
  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: 'diaries', component: DiariesIndexComponent,
    children: [
      { path: 'view', component: ViewDiariesComponent, canActivate: [AuthGuard] },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
