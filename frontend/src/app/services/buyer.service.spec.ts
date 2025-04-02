import { TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { BuyerService } from './buyer.service';

describe('BuyerService', () => {
  let service: BuyerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        BuyerService,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    });
    service = TestBed.inject(BuyerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});