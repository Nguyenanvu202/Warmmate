import { Component, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-errors',
  standalone: true,
  imports: [MatButton],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.scss'
})
export class TestErrorsComponent {
 baseUrl = 'https://localhost:5001/api/';
 private http = inject(HttpClient);
 validationErrors?: string[];

 get400Error(){
   this.http.get(this.baseUrl + 'bughandle/badrequest').subscribe({
     next: response => console.log(response),
     error: error => console.log(error)
   })
 }

 get401Error(){
  this.http.get(this.baseUrl + 'bughandle/unauthorize').subscribe({
    next: response => console.log(response),
    error: error => console.log(error)
  })
}

get404Error(){
  this.http.get(this.baseUrl + 'bughandle/notfound').subscribe({
    next: response => console.log(response),
    error: error => console.log(error)
  })
}

get500Error(){
  this.http.get(this.baseUrl + 'bughandle/internalerror').subscribe({
    next: response => console.log(response),
    error: error => console.log(error)
  })
}

getValidationError(){
  this.http.post(this.baseUrl + 'bughandle/validationerror', {}).subscribe({
    next: response => console.log(response),
    error: error => this.validationErrors = error
  })
}
}
