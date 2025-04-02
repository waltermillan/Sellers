import { TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { SaleDTOService } from './sale-dto.service';

describe('SaleDTOService', () => {
  let service: SaleDTOService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        SaleDTOService,
        provideHttpClient(withInterceptorsFromDi(), withFetch())
      ]      
    });

    service = TestBed.inject(SaleDTOService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
