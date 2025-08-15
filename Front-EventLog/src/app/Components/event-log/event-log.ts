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
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-event-log',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatTableModule,
    MatCardModule,
    MatSnackBarModule,
    MatIconModule
  ],
  templateUrl: './event-log.component.html',
  styleUrls: ['./event-log.css']
})
export class EventLogComponent implements OnInit {
  filterForm: FormGroup;
  events$!: Observable<EventLog[]>; // Observable de eventos
  displayedColumns: string[] = ['eventDate', 'description', 'eventType'];

  constructor(
    private fb: FormBuilder,
    private service: EventLogService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.filterForm = this.fb.group({
      eventType: [''],
      startDate: [null],
      endDate: [null]
    });
  }

  ngOnInit() {
    // Cargar todos los eventos al iniciar
    this.loadEvents();
  }

  // Abrir modal para registrar evento
  openRegisterModal() {
    const dialogRef = this.dialog.open(EventLogModalComponent, { width: '400px' });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.registerEvent(result);
    });
  }

  // Guardar evento
  registerEvent(eventData: any) {
    this.service.createEvent(eventData).subscribe({
      next: () => {
        this.snackBar.open('Evento guardado!', 'Cerrar', { duration: 8000 });
        this.loadEvents();
      },
      error: () => this.snackBar.open('Error guardando evento', 'Cerrar', { duration: 3000 })
    });
  }

  // Cargar eventos sin filtros
  loadEvents() {
    this.events$ = this.service.getAllEvents().pipe(
      catchError(() => {
        return of([]);
      })
    );
  }

  // Cargar eventos con filtros
  filter() {
    const { eventType, startDate, endDate } = this.filterForm.value;

    // Si no hay filtro de tipo, cargar todos
   
     if ((eventType==='') && (startDate != null)) {
      this.events$ = this.service.getEvents(eventType, startDate, endDate).pipe(
      catchError(() => {
        this.snackBar.open('Error cargando eventos', 'Cerrar', { duration: 3000 });
        return of([]);
      })
    );
    }else if((eventType !='') && (startDate != null)){
    
        this.events$ = this.service.getEvents(eventType, startDate, endDate).pipe(
      catchError(() => {
        this.snackBar.open('Error cargando eventos', 'Cerrar', { duration: 3000 });
        return of([]);
      })
    );
  }else if((eventType ==='') && (startDate === null)){ 
   this.loadEvents();
  }else if((eventType !='') && (startDate === null)){ 
   this.events$ = this.service.getEvents(eventType).pipe(
      catchError(() => {
        this.snackBar.open('Error cargando eventos', 'Cerrar', { duration: 3000 });
        return of([]);
      })
    );
  }
}
  }

