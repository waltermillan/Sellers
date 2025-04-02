import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { InfoBuyersComponent } from './info-buyers.component';

describe('InfoBuyersComponent', () => {
  let component: InfoBuyersComponent;
  let fixture: ComponentFixture<InfoBuyersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoBuyersComponent],
      providers: [
        InfoBuyersComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
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
