import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PairCreateDialogComponent } from './pair-create-dialog.component';

describe('PairCreateDialogComponent', () => {
  let component: PairCreateDialogComponent;
  let fixture: ComponentFixture<PairCreateDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PairCreateDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PairCreateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
