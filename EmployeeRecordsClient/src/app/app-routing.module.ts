import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import EmpDetailsComponent from './Employee/emp-details/emp-details.component';
import { EmpListComponent } from './Employee/emp-list/emp-list.component';
import { ErrorComponent } from './error/error.component';

const routes: Routes = [
  { path: "", component: EmpListComponent },
  { path: "employeeDetails/:id", component: EmpDetailsComponent },
  { path: "**", component: ErrorComponent, pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
