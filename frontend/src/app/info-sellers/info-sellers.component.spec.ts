import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';

import { InfoSellersComponent } from './info-sellers.component';

describe('InfoSellersComponent', () => {
  let component: InfoSellersComponent;
  let fixture: ComponentFixture<InfoSellersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoSellersComponent],
      providers: [
        InfoSellersComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch()) 
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoSellersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
