import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { DeleteSalesComponent } from './delete-sales.component';

describe('DeleteSalesComponent', () => {
  let component: DeleteSalesComponent;
  let fixture: ComponentFixture<DeleteSalesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeleteSalesComponent],
      providers: [
        DeleteSalesComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteSalesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
