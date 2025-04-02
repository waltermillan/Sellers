import { TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { ExportService } from './export.service';

describe('ExportService', () => {
  let service: ExportService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ExportService,
        provideHttpClient (withInterceptorsFromDi(), withFetch())
      ]      
    });

    service = TestBed.inject(ExportService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
