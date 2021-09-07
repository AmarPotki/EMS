import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FaulttypeComponent } from './faulttype.component';

describe('FaulttypeComponent', () => {
  let component: FaulttypeComponent;
  let fixture: ComponentFixture<FaulttypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FaulttypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FaulttypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
