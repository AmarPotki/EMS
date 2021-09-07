import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FixfaultListComponent } from './fixfault-list.component';

describe('FixfaultListComponent', () => {
  let component: FixfaultListComponent;
  let fixture: ComponentFixture<FixfaultListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FixfaultListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FixfaultListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
