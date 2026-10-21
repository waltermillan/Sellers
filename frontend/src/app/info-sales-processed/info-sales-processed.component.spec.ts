import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { InfoSalesProcessedComponent } from './info-sales-processed.component';

describe('InfoSalesComponent', () => {
  let component: InfoSalesProcessedComponent;
  let fixture: ComponentFixture<InfoSalesProcessedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoSalesProcessedComponent],
      providers: [
        InfoSalesProcessedComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoSalesProcessedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
