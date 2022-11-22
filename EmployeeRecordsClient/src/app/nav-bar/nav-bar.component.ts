import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddEmpComponent } from '../Employee/add-emp/add-emp.component';
import { EmployeeService } from '../_services/employee.service';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent {

  /**
   *
   */
  constructor(private dialog: MatDialog, private context: EmployeeService) {

  }
  openDialog() {
    const dialogRef = this.dialog.open(AddEmpComponent, {
      width: '40%'
    }).afterClosed().subscribe(e => {
      if (e === 'Add') {
        // this.context.setProduct(e);

      }
    })
    // dialogRef.afterClosed().subscribe(result => {
    //   console.log(`Dialog result: ${result}`);
    // });
  }

}
