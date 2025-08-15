import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

/**
 * Interfaz que representa un registro de evento.
 */
export interface EventLog {
  id?: number; // ID opcional (generado por el backend)
  eventDate: Date; // Fecha del evento
  description: string; // Descripción del evento
  eventType: string; // Tipo de evento: "API" o "Manual"
}

@Injectable({ providedIn: 'root' })
export class EventLogService {
  // URL base del endpoint de la API
  private apiUrl = 'https://localhost:7135/api/EventLogs'; // Ajusta según tu backend

  constructor(private http: HttpClient) {}

  /**
   * Crea un nuevo evento enviándolo al backend.
   * @param log Objeto EventLog con los datos del evento
   * @returns Observable con el evento creado
   */
  createEvent(log: EventLog): Observable<EventLog> {
    return this.http.post<EventLog>(this.apiUrl, log);
  }

  /**
   * Obtiene todos los eventos sin aplicar filtros.
   * @returns Observable con la lista de eventos
   */
  getAllEvents(): Observable<EventLog[]> {
    return this.http.get<EventLog[]>(this.apiUrl);
  }

  /**
   * Obtiene eventos filtrados por tipo y/o rango de fechas.
   * @param eventType Tipo de evento (opcional)
   * @param startDate Fecha de inicio del filtro (opcional)
   * @param endDate Fecha de fin del filtro (opcional)
   * @returns Observable con la lista de eventos filtrados
   */
  getEvents(eventType?: string, startDate?: Date, endDate?: Date): Observable<EventLog[]> {
    let params = new HttpParams();

    // Agrega parámetros si están definidos
    if (eventType) params = params.set('eventType', eventType);
    if (startDate) params = params.set('startDate', startDate.toISOString());
    if (endDate) params = params.set('endDate', endDate.toISOString());

    // Llama al endpoint de filtros
    return this.http.get<EventLog[]>(`${this.apiUrl}/filter`, { params });
  }
}
