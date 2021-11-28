import { Injectable } from '@angular/core';
import { UserManagerSettings, UserManager, User } from "oidc-client"
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userManager = new UserManager(this.getSettings());
  private user: User;

  constructor(private router: Router) { 
    this.userManager.events.addAccessTokenExpiring(function(){
      console.log("token expiring..." + Date.now());
    });

    this.userManager.events.addUserSignedOut(function() {
      console.log("user signed out !");
    });
  }

  public signout() {
    this.userManager.signoutRedirect();
  }

  public redirectToSts(state: string) {
    var redirectConfig = {
      state: state
    };
    this.userManager.signinRedirect(redirectConfig);
  }

  redirectCallback() {
    this.userManager.signinRedirectCallback().then(user=>{
        this.user = user;
        this.router.navigate([user.state]);
    })
  }

  public getUser() : User {
    return this.user;
  }

  public isUserLoggedIn(): boolean {
    return this.user != null;
  }

  getSettings(): UserManagerSettings {
    return {
      authority: 'https://localhost:5001/',
      client_id: 'diaries-front',
      redirect_uri: 'http://localhost:4200/auth-callback',
      response_type: "code", 
      scope: "openid profile",
      automaticSilentRenew: true,
      silent_redirect_uri: "http://localhost:4200/assets/silent-refresh.html"
    };
  }
}
