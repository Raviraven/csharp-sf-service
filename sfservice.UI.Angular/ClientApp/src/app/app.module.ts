import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Routes, RouterModule } from '@angular/router';
import { DungeonService } from './dungeons/services/dungeon.service';

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
    AppRoutingModule,
    HttpClientModule,
    RouterModule.forChild(routes)
  ],
  providers: [ DungeonService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
