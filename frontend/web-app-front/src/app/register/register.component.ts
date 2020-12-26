import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {


  registerForm: FormGroup;

  constructor(
    private router: Router,
    private fb: FormBuilder
  ) { 
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: [''],
      dob: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  ngOnInit(): void {
  }

  registerUser(){
    if(this.registerForm.status == "VALID"){
      console.log('Registering with data: ',this.registerForm.value);
    }
    else{
      console.log('Please check Required Fields');
    }
    
  }

}
