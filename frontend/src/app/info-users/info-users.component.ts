import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-info-users',
  templateUrl: './info-users.component.html',
  styleUrl: './info-users.component.css'
})
export class InfoUsersComponent implements OnInit {

  newUser:User = {
    id: 0,
    userName: '',
    passwordHash: '',
    createdAt: new Date(),
    updatedAt: new Date()
  }
  users: User[] = [];

  constructor(public messageService: MessageService,
              private userService:UserService) {
    
  }

  ngOnInit(){
    this.getAll();
  }

  getAll(){
    this.userService.getAll().subscribe({
      next: (data: User[]) => {
        this.users = data;
      },
      error: (error) => {
        console.log('Error al cargar datos del usuario.');
      }
    });
  }

  addUser(){
    this.userService.add(this.newUser).subscribe({
      next: (data) => {
        this.getAll();
        this.messageService.showMessage = true;
        this.messageService.message = 'User successfully added.';
      },
      error: (error) => {
        console.error('Error al agregar nuevo usuario', error);
      }
    });
  }

  deleteUser(id:number){
    this.userService.delete(id).subscribe({
      next: (data) => {
        this.getAll();
        this.messageService.showMessage = true;
        this.messageService.message = 'User successfully deleted.';
      },
      error: (error) => {
        console.error('Error al quitar usuario', error);
      }
    });
  }

  cannotDeleteUserMessage(){
    this.messageService.showMessage = true;
    this.messageService.message = 'This user is a system administrator and cannot be deleted.';
  }

  closeMessage(){
    this.messageService.closeMessage();
  }

}
