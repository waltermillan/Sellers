import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';  // Asegúrate de tener el servicio de autenticación

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
    // Llamada al servicio para hacer la autenticación
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        // Si la respuesta es exitosa, consideramos que el usuario está autenticado
        this.authService.loggedIn = true;  // Marca como autenticado
        this.router.navigate(['/principal']);  // Redirige al usuario después del login
      },
      error: (error) => {
        // Si hay un error (usuario o contraseña incorrectos), mostramos el mensaje de error
        this.loginError = 'Invalid username or password';
      }
  });
  }
}
