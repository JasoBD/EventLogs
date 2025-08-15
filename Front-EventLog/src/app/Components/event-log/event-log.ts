import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EventLogService, EventLog } from '../../Services/EventLog/event-log-service';
import { EventLogModalComponent } from './event-log-modal/event-log-modal.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-event-log', // Selector del componente
  standalone: true, // Componente independiente
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatTooltipModule,
    MatTableModule,
    MatCardModule,
    MatSnackBarModule,
    MatIconModule
  ],
  templateUrl: './event-log.component.html', // HTML asociado
  styleUrls: ['./event-log.css'] // Estilos asociados
})
export class EventLogComponent implements OnInit {
  filterForm: FormGroup; // Formulario reactivo para filtros
  events$!: Observable<EventLog[]>; // Observable que contiene los eventos
  displayedColumns: string[] = ['eventDate', 'description', 'eventType']; // Columnas de la tabla

  constructor(
    private fb: FormBuilder, // Para construir formularios reactivos
    private service: EventLogService, // Servicio que maneja los eventos
    private dialog: MatDialog, // Para abrir modales
    private snackBar: MatSnackBar // Para mostrar notificaciones
  ) {
    // Inicializa el formulario de filtros
    this.filterForm = this.fb.group({
      eventType: [''], // Tipo de evento
      startDate: [null], // Fecha de inicio
      endDate: [null] // Fecha de fin
    });
  }

  ngOnInit() {
    // Carga todos los eventos al iniciar el componente
    this.loadEvents();
  }

  /**
   * Abre el modal para registrar un nuevo evento.
   */
  openRegisterModal() {
    const dialogRef = this.dialog.open(EventLogModalComponent, { width: '400px' });
    dialogRef.afterClosed().subscribe(result => {
      // Si el usuario cerró el modal con datos, se registra el evento
      if (result) this.registerEvent(result);
    });
  }

  /**
   * Registra un nuevo evento usando el servicio.
   * @param eventData Datos del evento
   */
  registerEvent(eventData: any) {
    this.service.createEvent(eventData).subscribe({
      next: () => {
        // Notificación de éxito
        this.snackBar.open('Evento guardado!', 'Cerrar', { duration: 8000 });
        this.loadEvents(); // Recarga la tabla de eventos
      },
      error: () => this.snackBar.open('Error guardando evento', 'Cerrar', { duration: 5000 }) // Notificación de error
    });
  }

  /**
   * Carga todos los eventos sin aplicar filtros.
   */
  loadEvents() {
    this.events$ = this.service.getAllEvents().pipe(
      catchError(() => {
        // Si ocurre un error, retorna un array vacío
        return of([]);
      })
    );
  }

  /**
   * Filtra eventos según el formulario de filtros.
   */
  filter() {
    const { eventType, startDate, endDate } = this.filterForm.value;

    // Filtros combinados
    if (eventType === '' && startDate != null) {
      this.events$ = this.service.getEvents(eventType, startDate, endDate).pipe(
        catchError(() => {
          this.snackBar.open('Error cargando eventos', 'Cerrar', { duration: 5000 });
          return of([]);
        })
      );
    } else if (eventType !== '' && startDate != null) {
      this.events$ = this.service.getEvents(eventType, startDate, endDate).pipe(
        catchError(() => {
          this.snackBar.open('Error cargando eventos', 'Cerrar', { duration: 5000 });
          return of([]);
        })
      );
    } else if (eventType === '' && startDate === null) {
      this.loadEvents(); // Sin filtros, carga todos
    } else if (eventType !== '' && startDate === null) {
      this.events$ = this.service.getEvents(eventType).pipe(
        catchError(() => {
          this.snackBar.open('Error cargando eventos', 'Cerrar', { duration: 5000 });
          return of([]);
        })
      );
    }
  }
}
