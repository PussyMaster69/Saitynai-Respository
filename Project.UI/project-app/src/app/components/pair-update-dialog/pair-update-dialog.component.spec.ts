import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PairUpdateDialogComponent } from './pair-update-dialog.component';

describe('PairUpdateDialogComponent', () => {
  let component: PairUpdateDialogComponent;
  let fixture: ComponentFixture<PairUpdateDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PairUpdateDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PairUpdateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
