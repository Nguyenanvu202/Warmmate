import { Component } from '@angular/core';
import {HttpErrorResponse} from '@angular/common/http';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
@Component({
  selector: 'app-server-errors',
  standalone: true,
  imports: [MatCardModule],
  templateUrl: './server-errors.component.html',
  styleUrl: './server-errors.component.scss'
})
export class ServerErrorsComponent {
  error?: any;
  constructor(private router: Router){
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras.state?.['error'];
  }

}
