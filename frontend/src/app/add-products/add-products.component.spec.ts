import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AddProductsComponent } from './add-products.component';

describe('AddProductsComponent', () => {
  let component: AddProductsComponent;
  let fixture: ComponentFixture<AddProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [AddProductsComponent],
      providers: [
        AddProductsComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
