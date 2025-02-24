import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoSalesComponent } from './info-sales.component';

describe('InfoSalesComponent', () => {
  let component: InfoSalesComponent;
  let fixture: ComponentFixture<InfoSalesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoSalesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoSalesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
