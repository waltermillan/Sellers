import { TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { SaleProcessedService } from './saleProcessed.service';

describe('SaleProcessedService', () => {
  let service: SaleProcessedService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    providers: [
      SaleProcessedService,
      provideHttpClient(withInterceptorsFromDi(), withFetch())
    ]      
    });

    service = TestBed.inject(SaleProcessedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
