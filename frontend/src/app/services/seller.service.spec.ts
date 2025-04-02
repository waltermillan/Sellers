import { TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { SellerService } from './seller.service';


describe('SellerService', () => {
  let service: SellerService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        SellerService,
        provideHttpClient(withInterceptorsFromDi(), withFetch())
      ]
    });

    service = TestBed.inject(SellerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
