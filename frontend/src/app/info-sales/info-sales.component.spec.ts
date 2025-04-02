import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { InfoSalesComponent } from './info-sales.component';

describe('InfoSalesComponent', () => {
  let component: InfoSalesComponent;
  let fixture: ComponentFixture<InfoSalesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoSalesComponent],
      providers: [
        InfoSalesComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoSalesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
