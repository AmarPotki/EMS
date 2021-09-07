import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyfaultDetailComponent } from './myfault-detail.component';

describe('MyfaultDetailComponent', () => {
  let component: MyfaultDetailComponent;
  let fixture: ComponentFixture<MyfaultDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyfaultDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyfaultDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
