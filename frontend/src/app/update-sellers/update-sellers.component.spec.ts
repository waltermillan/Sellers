import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UpdateSellersComponent } from './update-sellers.component';

describe('UpdateSellersComponent', () => {
  let component: UpdateSellersComponent;
  let fixture: ComponentFixture<UpdateSellersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateSellersComponent],
      imports: [FormsModule],
      providers: [
        UpdateSellersComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateSellersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
