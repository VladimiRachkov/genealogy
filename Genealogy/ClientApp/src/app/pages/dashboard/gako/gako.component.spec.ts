/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { GakoComponent } from './gako.component';

describe('GakoComponent', () => {
  let component: GakoComponent;
  let fixture: ComponentFixture<GakoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GakoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GakoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
