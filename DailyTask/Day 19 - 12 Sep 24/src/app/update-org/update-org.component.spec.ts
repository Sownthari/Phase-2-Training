import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateOrgComponent } from './update-org.component';

describe('UpdateOrgComponent', () => {
  let component: UpdateOrgComponent;
  let fixture: ComponentFixture<UpdateOrgComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateOrgComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdateOrgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
