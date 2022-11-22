import { AfterViewChecked, Component, DoCheck, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Employee } from 'src/app/_models/employee';
import { EmployeeToAdd } from 'src/app/_models/employee-to-add';
import { EmployeeService } from 'src/app/_services/employee.service';
import { ToastrService } from 'ngx-toastr'
import { EmployeeImages } from 'src/app/_models/employee-images';
@Component({
  selector: 'app-emp-details',
  templateUrl: './emp-details.component.html',
  styleUrls: ['./emp-details.component.css']
})
export default class EmpDetailsComponent implements OnInit {
  private employeeId: number;
  public employeeDetails: Employee
  public empImages: EmployeeImages[]

  constructor(private route: ActivatedRoute, private context: EmployeeService, private toastr: ToastrService) {
    this.route.params.subscribe(params => {
      this.employeeId = params['id']
    });

  }


  ngOnInit(): void {
    this.GetEmployee();
    this.GetEmployeePics();
  }

  GetEmployee() {
    this.context.GetEmployee(this.employeeId).subscribe({
      next: (result: Employee) => {
        this.employeeDetails = result;
      }, error: (e) => {
        this.toastr.error("connection error " + e.message)
      }
    })
  }

  GetEmployeePics() {
    this.context.GetEmployeeImages(this.employeeId).subscribe({
      next: (result: Array<EmployeeImages>) => {
        this.empImages = result;
      }, error: (e) => {
        this.toastr.error("connection error " + e.message)
      }
    })
  }
}

