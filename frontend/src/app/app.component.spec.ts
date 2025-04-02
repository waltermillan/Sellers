import { TestBed } from '@angular/core/testing';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { AuthService } from './services/auth.service';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        FormsModule,
        RouterModule.forRoot([]),
      ],
      declarations: [
        AppComponent,
        LoginComponent,
      ],
      providers: [
        provideHttpClient(withInterceptorsFromDi(), withFetch()),
        AuthService
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });
});
