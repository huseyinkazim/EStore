import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenablebutonComponent } from './openablebuton.component';

describe('OpenablebutonComponent', () => {
  let component: OpenablebutonComponent;
  let fixture: ComponentFixture<OpenablebutonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OpenablebutonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OpenablebutonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
