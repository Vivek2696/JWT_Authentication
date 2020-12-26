import { Injectable, Injector } from '@angular/core';
import { LoginService } from './account/login.service';
import { RegisterService } from './account/register.service';

@Injectable({
  providedIn: 'root'
})
export class WebAppService {

  private _loginService!: LoginService;
  private _registerService!: RegisterService;

  constructor(private injector: Injector) { }

  public get loginService(): LoginService{
    if(!this._loginService){
      return this._loginService = this.injector.get(LoginService);
    }
    return this._loginService;
  }

  public get registerService(): RegisterService{
    if(!this._loginService){
      return this._registerService = this.injector.get(RegisterService);
    }
    return this._registerService;
  }



  authenticateUser(data: any){
    return this.loginService.authenticateUser(data);
  }

  registerUser(data: any){
    return this.registerService.registerUser(data);
  }

}
