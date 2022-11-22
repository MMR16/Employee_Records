import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmpListComponent } from './Employee/emp-list/emp-list.component';
import { HttpClientModule } from '@angular/common/http';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { AddEmpComponent } from './Employee/add-emp/add-emp.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularMaterialsModule } from './Modules/angular-materials.module';
import { ErrorComponent } from './error/error.component';
import EmpDetailsComponent from './Employee/emp-details/emp-details.component';
import { ToastrModule } from 'ngx-toastr'
@NgModule({
  declarations: [AppComponent, EmpListComponent, NavBarComponent, AddEmpComponent, ErrorComponent, EmpDetailsComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule, ReactiveFormsModule,
    ToastrModule.forRoot({
      positionClass: "toast-bottom-right"
    }),
    AngularMaterialsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
