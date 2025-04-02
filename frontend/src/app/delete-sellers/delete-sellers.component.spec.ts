import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { DeleteSellersComponent } from './delete-sellers.component';

describe('DeleteSellersComponent', () => {
  let component: DeleteSellersComponent;
  let fixture: ComponentFixture<DeleteSellersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeleteSellersComponent],
      providers: [
        DeleteSellersComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteSellersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
