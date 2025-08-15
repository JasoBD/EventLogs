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
import {  provideNativeDateAdapter } from '@angular/material/core';


@Component({
  selector: 'app-event-log-modal',
  standalone: true,
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
    provideNativeDateAdapter(),    // Provee DateAdapter nativo
  ],
   
  templateUrl: './event-log-modal.component.html',
  styleUrls: ['./event-log-modal.component.css']

})
export class EventLogModalComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EventLogModalComponent>
  ) {
    this.form = this.fb.group({
      eventDate: [null, Validators.required],
      description: ['', Validators.required],
      eventType: ['Manual', Validators.required]
    });
  }

   save() {
    if (this.form.valid) {
      this.dialogRef.close({
        ...this.form.value,
        eventDate: this.form.value.eventDate || new Date(),
        eventType: 'Manual'
      });
    }
  }

  close() {
    this.dialogRef.close();
  }
}
