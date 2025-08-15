import { Routes } from '@angular/router';
import { EventLogComponent } from './Components/event-log/event-log';


export const routes: Routes = [
  { path: '', redirectTo: 'eventlog', pathMatch: 'full' }, // redirección por defecto
  { path: 'eventlog', component: EventLogComponent }
];