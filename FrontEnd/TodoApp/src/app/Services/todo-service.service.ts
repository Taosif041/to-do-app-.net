import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';

export interface TodoItem{
  id: number,
  name: string| null,
  isCompleted: boolean
}

@Injectable({
  providedIn: 'root'
})
export class TodoServiceService {
  private apiUrl = 'http://localhost:5254/api/Todo';

  constructor(private http: HttpClient) { }

  getTodos(): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]> (this.apiUrl);
  }

  getTodo(id: number): Observable<TodoItem> {
    return this.http.get<TodoItem> (`${this.apiUrl}/${id}`);
  }

  postTodo(item: TodoItem): Observable<TodoItem> {
    return this.http.post<TodoItem> (this.apiUrl, item);
  }

  updateTodo(item: TodoItem): Observable<TodoItem> {
    return this.http.put<TodoItem>  (`${this.apiUrl}/${item.id}`, item);
  }

  deleteTodo(id: number): Observable<TodoItem> {
    return this.http.delete<TodoItem> (`${this.apiUrl}/${id}`);
  }


}
