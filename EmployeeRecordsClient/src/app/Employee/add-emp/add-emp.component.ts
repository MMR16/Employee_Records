import { Component, OnInit, Inject } from '@angular/core';
import { Department } from 'src/app/_models/department';
import { FormGroup, FormBuilder, Validators, NgModel } from '@angular/forms';
import { EmployeeService } from 'src/app/_services/employee.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeToEdit } from 'src/app/_models/employee-to-Edit';
import { ToastrService } from 'ngx-toastr'


@Component({
  selector: 'app-add-emp',
  templateUrl: './add-emp.component.html',
  styleUrls: ['./add-emp.component.css'],
})
export class AddEmpComponent implements OnInit {
  departmentModel: Department[];
  employeeForm: FormGroup;
  actionBtn: string = 'Add'
  departmentId: number

  constructor(
    private formBuilder: FormBuilder,
    private context: EmployeeService,
    private dialogRef: MatDialogRef<AddEmpComponent>,
    private toastr: ToastrService,
    @Inject(MAT_DIALOG_DATA) public editEmp: EmployeeToEdit
  ) { }

  ngOnInit(): void {

    this.context.GetDepartments().subscribe({
      next: (result: Department[]) => {
        this.departmentModel = result;
      },
      error: (e) => {
        this.toastr.error("error connecting to server " + e.message)
      },
    });
    // this.departmentModel.includes()
    if (this.editEmp) {
      this.context.GetDepartment(this.editEmp["department"]).subscribe({
        next: e => {
          this.departmentId = e
        },
        error: (e) => {
          this.toastr.error("error connecting to server " + e.message)
        },

      });
    }
    this.employeeForm = this.formBuilder.group({
      id: ['', Validators.min(0)],
      name: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      adress: ['', Validators.required],
      departmentId: ['', Validators.required],
    });

    if (this.editEmp) {
      this.employeeForm.controls['name'].setValue(this.editEmp.name)
      this.employeeForm.controls['dateOfBirth'].setValue(this.editEmp.dateOfBirth)
      this.employeeForm.controls['adress'].setValue(this.editEmp.adress)
      this.employeeForm.controls['departmentId'].setValue(this.editEmp.departmentId)
      this.actionBtn = 'Update'
    }

  }

  addEmployee() {
    if (!this.editEmp) {

      if (this.employeeForm.valid) {
        this.context.AddEmployee(this.employeeForm.value).subscribe({
          next: () => {
            this.context.setEmployee(this.employeeForm.value);
            this.toastr.success("Employee Added Successfully ")
            this.employeeForm.reset();
            this.dialogRef.close('Add');
          },
          error: (e) => {
            this.toastr.error("error while Adding Employee " + e.message)
            console.log(e.message);
          },
        });
      }
    } else {
      this.updateEmp()
    }
  }

  updateEmp() {
    if (this.employeeForm.valid) {
      this.context.EditEmployee(this.editEmp.id, this.employeeForm.value).subscribe({
        next: () => {
          this.toastr.success("Employee Updated Successfully ")
          this.employeeForm.reset();
          this.dialogRef.close('Update');
        },
        error: (e) => {
          this.toastr.error("error while Updating Employee " + e.message)
        },
      });
    }
  }
}
