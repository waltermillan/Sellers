import { TestBed } from '@angular/core/testing';

import { SaleService } from './sale.service';

describe('SaletService', () => {
  let service: SaleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SaleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
