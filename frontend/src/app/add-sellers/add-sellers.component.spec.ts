import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient, withInterceptorsFromDi, withFetch } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';  // Asegúrate de importar ReactiveFormsModule si usas formularios reactivos
import { AddSellersComponent } from './add-sellers.component';

describe('AddSellersComponent', () => {
  let component: AddSellersComponent;
  let fixture: ComponentFixture<AddSellersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        FormsModule,  // Si usas formularios basados en plantillas
        ReactiveFormsModule,  // Si usas formularios reactivos, añade este módulo
      ],
      declarations: [AddSellersComponent],
      providers: [
        provideHttpClient(withInterceptorsFromDi(), withFetch())  // Si usas HttpClient con interceptores, se necesita aquí
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(AddSellersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
