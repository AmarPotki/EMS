import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FixfaultListItemComponent } from './fixfault-list-item.component';

describe('FixfaultListItemComponent', () => {
  let component: FixfaultListItemComponent;
  let fixture: ComponentFixture<FixfaultListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FixfaultListItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FixfaultListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
