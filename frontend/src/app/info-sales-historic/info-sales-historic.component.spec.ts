import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { InfoSalesHistoricComponent } from './info-sales-historic.component';

describe('InfoSalesHistoricComponent', () => {
  let component: InfoSalesHistoricComponent;
  let fixture: ComponentFixture<InfoSalesHistoricComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoSalesHistoricComponent],
      providers: [
        InfoSalesHistoricComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoSalesHistoricComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
