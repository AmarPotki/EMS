import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FixfaultDetailComponent } from './fixfault-detail.component';

describe('FixfaultDetailComponent', () => {
  let component: FixfaultDetailComponent;
  let fixture: ComponentFixture<FixfaultDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FixfaultDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FixfaultDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
