import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UpdateProductsComponent } from './update-products.component';

describe('UpdateProductsComponent', () => {
  let component: UpdateProductsComponent;
  let fixture: ComponentFixture<UpdateProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [UpdateProductsComponent],
      providers: [
        UpdateProductsComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch())
      ]

    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
