import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { provideNativeDateAdapter } from '@angular/material/core';

@Component({
  selector: 'app-event-log-modal', // Selector del componente
  standalone: true, // Componente independiente (no necesita NgModule)
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatButtonModule,
    MatTableModule,
    MatCardModule,
    MatSnackBarModule,
    MatIconModule
  ],
  providers: [
    provideNativeDateAdapter(), // Provee DateAdapter nativo para DatePicker
  ],
  templateUrl: './event-log-modal.component.html', // Archivo HTML asociado
  styleUrls: ['./event-log-modal.component.css'] // Estilos CSS asociados
})
export class EventLogModalComponent {
  form: FormGroup; // FormGroup para manejar el formulario reactivo

  constructor(
    private fb: FormBuilder, // FormBuilder para crear el FormGroup
    private dialogRef: MatDialogRef<EventLogModalComponent> // Referencia al diálogo abierto
  ) {
    // Inicializa el formulario con campos: eventDate, description y eventType
    this.form = this.fb.group({
      eventDate: [null, Validators.required], // Fecha del evento (obligatoria)
      description: ['', Validators.required], // Descripción del evento (obligatoria)
      eventType: ['Manual', Validators.required] // Tipo de evento por defecto "Manual" (obligatorio)
    });
  }

  /**
   * Método que se ejecuta al guardar el formulario.
   * Cierra el modal y devuelve los datos ingresados.
   */
  save() {
    if (this.form.valid) { // Solo si el formulario es válido
      this.dialogRef.close({
        ...this.form.value, // Copia los valores del formulario
        eventDate: this.form.value.eventDate || new Date(), // Si no se selecciona fecha, toma la fecha actual
        eventType: 'Manual' // Asegura que el tipo sea "Manual"
      });
    }
  }

  /**
   * Método que cierra el modal sin guardar cambios.
   */
  close() {
    this.dialogRef.close();
  }
}
