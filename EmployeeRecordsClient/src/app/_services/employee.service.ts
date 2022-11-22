import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EmployeeToAdd } from '../_models/employee-to-add';
import { EmployeeToEdit } from '../_models/employee-to-Edit';
import { Employee } from '../_models/employee';
import { Department } from '../_models/department';
import { BehaviorSubject } from 'rxjs';
import { EmployeeImages } from '../_models/employee-images';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private baseUrl: string = "http://localhost:5217/api/";

  private employee$ = new BehaviorSubject<any>({});
  selectedEmployee$ = this.employee$.asObservable();


  setEmployee(emp: any) {
    this.employee$.next(emp);
  }

  constructor(private http: HttpClient) { }

  GetEmployees() {
    return this.http.get<Array<Employee>>(`${this.baseUrl}Employee`)
  }

  GetDepartments() {
    return this.http.get<Array<Department>>(`${this.baseUrl}Department/`)
  }

  AddEmployee(emp: EmployeeToAdd) {
    return this.http.post<EmployeeToAdd>(`${this.baseUrl}Employee/`, emp);
  }

  EditEmployee(id: number, emp: EmployeeToAdd) {
    return this.http.put<EmployeeToEdit>(`${this.baseUrl}Employee/${id}`, emp);
  }

  DeleteEmployee(id: number) {
    return this.http.delete(`${this.baseUrl}Employee/${id}`);
  }

  GetDepartment(name: string) {
    return this.http.get<number>(`${this.baseUrl}Department/${name}`);
  }

  GetEmployee<EmployeeToEdit>(id: number) {
    return this.http.get<EmployeeToEdit>(`${this.baseUrl}Employee/${id}`);
  }

  GetEmployeeImages<EmployeeImages>(id: number) {
    return this.http.get<Array<EmployeeImages>>(`${this.baseUrl}Employee/${id}/empImages`);
  }

}
