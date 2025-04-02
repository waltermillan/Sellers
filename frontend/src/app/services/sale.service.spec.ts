import { TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { SaleService } from './sale.service';

describe('SaletService', () => {
  let service: SaleService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    providers: [
      SaleService,
      provideHttpClient(withInterceptorsFromDi(), withFetch())
    ]      
    });

    service = TestBed.inject(SaleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
