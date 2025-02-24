import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteBuyersComponent } from './delete-buyers.component';

describe('DeleteBuyersComponent', () => {
  let component: DeleteBuyersComponent;
  let fixture: ComponentFixture<DeleteBuyersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeleteBuyersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteBuyersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
