import { TestBed } from '@angular/core/testing';

import { SaleDTOService } from './sale-dto.service';

describe('SaleDTOService', () => {
  let service: SaleDTOService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SaleDTOService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
