import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { WebAppService } from '../service/web-app.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {


  registerForm: FormGroup;
  newPassword: string = '';
  confirmPassword: string = '';
  samePasswords: boolean = true;
  isLoading: boolean = false;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private jwtService: WebAppService,
    private snackBar: MatSnackBar
  ) { 
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: [''],
      dob: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    })
  }

  ngOnInit(): void {
  }

  registerUser(){
    if(!this.checkPassword()){
      this.samePasswords = false;
      console.log('Please check requirements');
    }
    else if(this.registerForm.status == "VALID"){
      console.log('Registering with data: ',this.registerForm.value);
      //Disable the form
      this.registerForm.disable();
      this.isLoading = true;

      //create loading message
      this.snackBar.open('Loading...', 'Close');
      //Ready the object
      let userModel = {
        FirstName: this.registerForm.get('firstName')?.value,
        LastName: this.registerForm.get('lastName')?.value,
        Dob: this.registerForm.get('dob')?.value,
        Email: this.registerForm.get('email')?.value,
        Password: this.registerForm.get('password')?.value
      }
      this.jwtService.registerUser(userModel).subscribe(res => {
        if(res != undefined){
          //Show message
          this.snackBar.dismiss();
          this.snackBar.open('Registered Successfully!', 'Close', {
            duration: 3000,
          });
          //Route to login page
          this.router.navigate(['login']);
        }
        else{
          this.snackBar.dismiss();
          this.snackBar.open('Error! Please try again', 'Close', {
            duration: 3000,
          });
          this.registerForm.enable();
          this.isLoading = false;
        }
      })
    }
    else{
      console.log('Please check required fields');
    }
  }

  checkPassword(): boolean{
    let password = this.registerForm.get('password')?.value;
    console.log('Entered New Password: ', password);
    let confirmPassword = this.registerForm.get('confirmPassword')?.value;
    console.log('Entered to confirm: ', confirmPassword);
    if(password == confirmPassword){
      this.samePasswords = true;
      return true;
    }
    else{
      this.samePasswords = false;
      return false;
    }
  }

}
