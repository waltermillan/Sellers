import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoProductsComponent } from './info-products.component';

describe('InfoProductsComponent', () => {
  let component: InfoProductsComponent;
  let fixture: ComponentFixture<InfoProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoProductsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
