import { Component, inject } from '@angular/core';
 import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
 import { MatButton } from '@angular/material/button';
 import { MatCard } from '@angular/material/card';
 import { MatFormField, MatLabel } from '@angular/material/form-field';
 import { MatInput } from '@angular/material/input';
 import { AccountService } from '../../../core/services/account.service';
 import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatDialogRef } from '@angular/material/dialog';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [    ReactiveFormsModule,
    MatCard,
    MatFormField,
    MatInput,
    MatLabel,
    MatButton,
    RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private accountService = inject(AccountService);
  private activatedRoute = inject(ActivatedRoute);
  protected dialogRef = inject(MatDialogRef<LoginComponent>);

  loginForm = this.fb.group({
    email: [''],
    password: ['']
  });

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        this.accountService.getUserInfo().subscribe();
        this.router.navigateByUrl(
          this.activatedRoute.snapshot.queryParams['returnUrl'] || '/'
        );
        console.log("login success");
      }
    })
    this.dialogRef.close();
  }

  closeDialog(){
    
  }
}
