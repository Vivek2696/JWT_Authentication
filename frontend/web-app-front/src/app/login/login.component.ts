import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

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
    private router: Router
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
    //call service
    //this.isLoading = false;
  }

}
