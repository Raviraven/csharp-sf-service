import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DungeonsAppComponent } from './dungeons-app.component';
import { Routes, RouterModule, Router } from '@angular/router';

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
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class DungeonsModule { }
