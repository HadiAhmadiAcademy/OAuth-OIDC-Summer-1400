import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexComponent } from './index/index.component';
import { DiariesIndexComponent } from './diaries-index/diaries-index.component';
import { ViewDiariesComponent } from './view-diaries/view-diaries.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthCallbackComponent } from './auth/auth-callback/auth-callback.component';

@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    DiariesIndexComponent,
    ViewDiariesComponent,
    AuthCallbackComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
