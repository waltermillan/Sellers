import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoBuyersComponent } from './info-buyers.component';

describe('InfoBuyersComponent', () => {
  let component: InfoBuyersComponent;
  let fixture: ComponentFixture<InfoBuyersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoBuyersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoBuyersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
