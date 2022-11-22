import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { Employee } from 'src/app/_models/employee';
import { EmployeeService } from 'src/app/_services/employee.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AddEmpComponent } from '../add-emp/add-emp.component';
import { ToastrService } from 'ngx-toastr'

@Component({
  selector: 'app-emp-list',
  templateUrl: './emp-list.component.html',
  styleUrls: ['./emp-list.component.css']
})
export class EmpListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'adress', 'dateOfBirth', 'department', 'Modify'];
  dataSource: MatTableDataSource<Employee>;
  selected: any

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private context: EmployeeService, private dialog: MatDialog, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.GetEmployees();

  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


  editEmp(emp: Employee) {
    this.dialog.open(AddEmpComponent, {
      width: '40%', data: emp
    }).afterClosed().subscribe((e) => {
      if (e === 'Update') {
        this.GetEmployees();
      }
    });;
  }

  deleteEmp(id: number) {
    this.context.DeleteEmployee(id).subscribe({
      next: () => {
        this.GetEmployees();
        this.toastr.success("employee deleted")
      },
      error: (e) => {
        this.toastr.error("error while Deleting Employee  " + e.message)
      }
    })
  }

  GetEmployees() {
    this.context.GetEmployees().subscribe({
      next:
        (result: Employee[]) => {
          this.dataSource = new MatTableDataSource(result);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }, error: e => {
          this.toastr.error("error while connecting to server " + e.message)
        }
    })
  }
}

