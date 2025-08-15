import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface EventLog {
  id?: number;
  eventDate: Date;
  description: string;
  eventType: string;
}

@Injectable({ providedIn: 'root' })
export class EventLogService {
  private apiUrl = 'https://localhost:7135/api/EventLogs'; // Ajusta tu endpoint

  constructor(private http: HttpClient) {}

  createEvent(log: EventLog): Observable<EventLog> {
    return this.http.post<EventLog>(this.apiUrl, log);
  }


  getAllEvents(): Observable<EventLog[]> {
    return this.http.get<EventLog[]>(this.apiUrl);
  }
  getEvents(eventType?: string, startDate?: Date, endDate?: Date): Observable<EventLog[]> {
    let params = new HttpParams();
    if (eventType) params = params.set('eventType', eventType);
    if (startDate) params = params.set('startDate', startDate.toISOString());
    if (endDate) params = params.set('endDate', endDate.toISOString());

    return this.http.get<EventLog[]>(this.apiUrl+'/filter', { params });
  }
}
