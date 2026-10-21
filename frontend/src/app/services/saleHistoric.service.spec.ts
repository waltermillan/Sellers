import { TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { SaleHistoricService } from './saleHistoric.service';

describe('SaleHistoricService', () => {
  let service: SaleHistoricService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    providers: [
      SaleHistoricService,
      provideHttpClient(withInterceptorsFromDi(), withFetch())
    ]      
    });

    service = TestBed.inject(SaleHistoricService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
