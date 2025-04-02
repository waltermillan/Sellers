import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { InfoUsersComponent } from './info-users.component';

describe('InfoUsersComponent', () => {
  let component: InfoUsersComponent;
  let fixture: ComponentFixture<InfoUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [InfoUsersComponent],
      providers: [
        InfoUsersComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
