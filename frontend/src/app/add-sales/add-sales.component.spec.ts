import { ComponentFixture, TestBed } from '@angular/core/testing';
import  { provideHttpClient, withInterceptorsFromDi, withFetch} from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AddSalesComponent } from './add-sales.component';


describe('AddSalesComponent', () => {
  let component: AddSalesComponent;
  let fixture: ComponentFixture<AddSalesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormsModule], 
      declarations: [AddSalesComponent],
      providers: [
        AddSalesComponent,
        provideHttpClient(withInterceptorsFromDi(), withFetch())
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddSalesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
