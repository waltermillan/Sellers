import { Component } from '@angular/core';
import { GLOBAL_CONFIG } from '../config/config.global';

@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html',
  styleUrl: './principal.component.css'
})
export class PrincipalComponent {
  title = GLOBAL_CONFIG.welcomeMessage;
}
