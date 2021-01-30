/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CustomSelectService } from './custom-select.service';

describe('Service: CustomSelect', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CustomSelectService]
    });
  });

  it('should ...', inject([CustomSelectService], (service: CustomSelectService) => {
    expect(service).toBeTruthy();
  }));
});
