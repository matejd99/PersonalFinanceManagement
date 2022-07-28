import { TestBed } from '@angular/core/testing';

import { PFMApiService } from './pfm-api.service';

describe('PFMApiService', () => {
  let service: PFMApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PFMApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
