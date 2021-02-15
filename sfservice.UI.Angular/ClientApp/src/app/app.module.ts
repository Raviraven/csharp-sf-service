import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Routes } from '@angular/router';

const routes: Routes = [
  {path: 'dungeons', loadChildren: () => import('./dungeons/dungeons.module').then(m=>m.DungeonsModule)},
  {path: '**', redirectTo: 'dungeons'}
];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
