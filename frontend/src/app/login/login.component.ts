import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  loginError: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {

        this.authService.loggedIn = true;
        this.router.navigate(['/principal']);
      },
      error: (error) => {

        console.log(error);
        if (error.status === 500)
          this.loginError = 'Connection to the web API failed. Please check your network connection.';
        else
          this.loginError = 'Invalid username or password';
      }
  });
  }
}
