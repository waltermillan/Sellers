import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { GLOBAL_CONFIG } from './config/config.global';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = GLOBAL_CONFIG.appName;
  
  constructor(public authService: AuthService) {}

  logout() {
    this.authService.logout();  // Cierra la sesión
  }
}
