import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateSellersComponent } from './update-sellers.component';

describe('UpdateSellersComponent', () => {
  let component: UpdateSellersComponent;
  let fixture: ComponentFixture<UpdateSellersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateSellersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateSellersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
