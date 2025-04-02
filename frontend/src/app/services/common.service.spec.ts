import { TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { CommonService } from './common.service';
import { BuyerService } from './buyer.service';

describe('CommonService', () => {
  let service: CommonService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        BuyerService,
        provideHttpClient(withInterceptorsFromDi(), withFetch())
      ]      
    });

    service = TestBed.inject(CommonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
