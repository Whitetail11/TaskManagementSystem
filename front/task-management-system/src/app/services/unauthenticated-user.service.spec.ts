import { TestBed } from '@angular/core/testing';

import { UnauthenticatedUserService } from './unauthenticated-user.service';

describe('UnauthenticatedUserService', () => {
  let service: UnauthenticatedUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UnauthenticatedUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
