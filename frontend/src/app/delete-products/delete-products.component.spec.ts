import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { DeleteProductsComponent } from './delete-products.component';

describe('DeleteProductsComponent', () => {
  let component: DeleteProductsComponent;
  let fixture: ComponentFixture<DeleteProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeleteProductsComponent],
      providers: [
        DeleteProductsComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch())
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
