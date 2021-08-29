import { TestBed } from '@angular/core/testing';

import { LoginStatusServiceService } from './login-status-service.service';

describe('LoginStatusServiceService', () => {
  let service: LoginStatusServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginStatusServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
