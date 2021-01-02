import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { WebAppService } from '../service/web-app.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  isLoading: boolean = false;

  constructor(
    public fb: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar,
    private jwtService: WebAppService
  ) {
    localStorage.clear();
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.isLoading = false;
  }

  authUser(){
    this.isLoading = true;
    //open message and create object
    this.snackBar.open('Loading...', 'Close');
    let loginCredential = {
      Email: this.loginForm.get('email')?.value,
      Password: this.loginForm.get('password')?.value
    }

    //call service
    this.jwtService.authenticateUser(loginCredential).subscribe(res => {
      if(typeof res === 'object' && res.hasOwnProperty('jwt')){
        this.snackBar.dismiss();
        this.snackBar.open('Login successfully!', 'Close', {
          duration: 3000,
        });
        console.log('Result from Login', JSON.stringify(res));
        localStorage.setItem('userObj', JSON.stringify(res));
        //navigate to home page
        this.isLoading = false;
        this.router.navigate(['home']);
      }
      else{
        this.snackBar.dismiss();
        this.snackBar.open('Error, Please try again!', 'Close', {
          duration: 3000,
        });
        this.isLoading = false;
      }
    })
  }

}
