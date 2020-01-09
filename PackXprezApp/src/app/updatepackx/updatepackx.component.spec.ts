import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatepackxComponent } from './updatepackx.component';

describe('UpdatepackxComponent', () => {
  let component: UpdatepackxComponent;
  let fixture: ComponentFixture<UpdatepackxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdatepackxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdatepackxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
