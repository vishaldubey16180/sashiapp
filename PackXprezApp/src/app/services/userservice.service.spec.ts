import { TestBed } from '@angular/core/testing';

import { UserServiceService } from './userservice.service';

describe('UserserviceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserServiceService = TestBed.get(UserServiceService);
    expect(service).toBeTruthy();
  });
});
