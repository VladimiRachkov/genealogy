/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { NecropolisComponent } from './necropolis.component';

describe('NecropolisComponent', () => {
  let component: NecropolisComponent;
  let fixture: ComponentFixture<NecropolisComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NecropolisComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NecropolisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
