import { TestBed } from '@angular/core/testing';

import { FaulttypeService } from './faulttype.service';

describe('FaulttypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FaulttypeService = TestBed.get(FaulttypeService);
    expect(service).toBeTruthy();
  });
});
