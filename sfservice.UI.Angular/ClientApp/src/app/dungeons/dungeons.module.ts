import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DungeonsAppComponent } from './dungeons-app.component';
import { Routes } from '@angular/router';

const routes: Routes = [
  {path: '', component: DungeonsAppComponent,
    children: [
      {path: '', component: DungeonsAppComponent}
    ]},
  {path: '**', redirectTo: ''}
];

@NgModule({
  declarations: [DungeonsAppComponent],
  imports: [
    CommonModule
  ]
})
export class DungeonsModule { }
