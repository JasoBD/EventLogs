import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { EventLogComponent } from './app/Components/event-log/event-log'; 

bootstrapApplication(EventLogComponent, appConfig)
  .catch((err) => console.error(err));
