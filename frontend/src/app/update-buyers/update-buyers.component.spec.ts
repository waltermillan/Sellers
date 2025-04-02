import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UpdateBuyersComponent } from './update-buyers.component';

describe('UpdateBuyersComponent', () => {
  let component: UpdateBuyersComponent;
  let fixture: ComponentFixture<UpdateBuyersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [UpdateBuyersComponent],
      providers: [
        UpdateBuyersComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateBuyersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
