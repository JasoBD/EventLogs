import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { EventLogComponent } from './Components/event-log/event-log';

@Component({
  selector: 'app-root',
  imports: [EventLogComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Front-EventLog');
}
