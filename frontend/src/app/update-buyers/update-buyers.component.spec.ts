import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateBuyersComponent } from './update-buyers.component';

describe('UpdateBuyersComponent', () => {
  let component: UpdateBuyersComponent;
  let fixture: ComponentFixture<UpdateBuyersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateBuyersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateBuyersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
